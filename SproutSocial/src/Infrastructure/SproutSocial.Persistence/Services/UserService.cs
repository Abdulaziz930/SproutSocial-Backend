using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Exceptions;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.Enums;

namespace SproutSocial.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
    {
        AppUser user = new()
        {
            Fullname = model.Fullname,
            UserName = model.Username,
            Email = model.Email
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            return new()
            {
                IsSuccess = true,
                Message = "User successfully created"
            };
        }

        throw new UserCreateFailedException();
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenEndDate.AddMinutes(refreshTokenLifeTime);

            await _userManager.UpdateAsync(user);
            return;
        }

        throw new UserNotFoundException("User cannot be null");
    }
}
