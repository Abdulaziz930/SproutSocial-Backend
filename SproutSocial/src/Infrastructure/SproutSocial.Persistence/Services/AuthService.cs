using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Email;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Abstractions.Token;
using SproutSocial.Application.DTOs.MailDtos;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Authentication.Token;
using SproutSocial.Application.Exceptions.Users;
using SproutSocial.Application.Helpers.Extesions;
using SproutSocial.Domain.Entities.Identity;
using System.Net;

namespace SproutSocial.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;
    private readonly IMailService _mailService;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
        , ITokenHandler tokenHandler, IUserService userService, IMailService mailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _userService = userService;
        _mailService = mailService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);

        if (user == null)
            throw new AuthenticationFailException();

        if (!await _userManager.IsEmailConfirmedAsync(user))
            throw new EmailNotConfirmedException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (result.Succeeded)
        {
            if (user.TwoFactorEnabled)
            {
                await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

                string twoFaCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "2FA Code", Body = $"Here is your code: {twoFaCode}" });

                return new() { RequiresTwoFactor = true };
            }
            var tokenResponse = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.Expiration, 2);
            return new()
            {
                RequiresTwoFactor = false,
                TokenResponse = tokenResponse
            };
        }

        throw new AuthenticationFailException();
    }

    public async Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
    {
        confirmEmailDto.Token.ThrowIfNullOrWhiteSpace(message: "Token cannot be null");
        confirmEmailDto.Email.ThrowIfNullOrWhiteSpace(message: "Email cannot be null");

        var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
        if(user is null)
            throw new UserNotFoundException($"User not found by email: {confirmEmailDto.Email}", HttpStatusCode.BadRequest);

        if (await _userManager.IsEmailConfirmedAsync(user))
            throw new EmailConfirmationException("This account already activated");

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
        if(!result.Succeeded)
            throw new EmailConfirmationException(result.Errors);

        return new(true, $"User successfully activated. Username: {user.UserName}");
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

    public async Task<TokenResponseDto> TwoFaLoginAsync(TwoFaLoginDto twoFaLoginDto, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByEmailAsync(twoFaLoginDto.Email);
        var result = await _signInManager.TwoFactorSignInAsync("Email", twoFaLoginDto.Code, false, false);
        if (result.Succeeded)
        {
            var tokenResponse = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.Expiration, 2);
            return tokenResponse;
        }

        throw new AuthenticationFailException("Invalid Code");
    }
}
