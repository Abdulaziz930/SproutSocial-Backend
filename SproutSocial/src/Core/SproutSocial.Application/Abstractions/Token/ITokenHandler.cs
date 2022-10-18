using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Application.Abstractions.Token;

public interface ITokenHandler
{
    Task<TokenResponseDto> CreateAccessTokenAsync(int second, AppUser user);
    string CreateRefreshToken();
}
