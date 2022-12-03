using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Abstractions.Token;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Authentication.Token;
using SproutSocial.Application.Exceptions.Users;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
        , ITokenHandler tokenHandler, IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _userService = userService;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);

        if (user == null)
            throw new AuthenticationFailException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (result.Succeeded)
        {
            var tokenResponse = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.Expiration, 2);
            return tokenResponse;
        }

        throw new AuthenticationFailException();
    }

    public async Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if(user is null)
            throw new UserNotFoundException("User not found");

        if (user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            TokenResponseDto tokenResponse = await _tokenHandler.CreateAccessTokenAsync(2, user);
            await _userService.UpdateRefreshToken(refreshToken, user, tokenResponse.Expiration, 2);
            return tokenResponse;
        }

        throw new RefreshTokenExpiredException();
    }
}
