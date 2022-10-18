using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SproutSocial.Application.Abstractions.Token;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SproutSocial.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;

    public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<TokenResponseDto> CreateAccessTokenAsync(int second, AppUser user)
    {
        TokenResponseDto tokenResponseDto = new();

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        tokenResponseDto.Expiration = DateTime.UtcNow.AddMinutes(second);
        JwtSecurityToken jwtSecurityToken = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: tokenResponseDto.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: claims
            );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        tokenResponseDto.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        tokenResponseDto.RefreshToken = CreateRefreshToken();

        return tokenResponseDto;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}
