using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
    Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime);
}
