using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SproutSocial.Core.Entities;
using SproutSocial.Data.Identity;
using SproutSocial.Service.Dtos.Account;
using SproutSocial.Service.Exceptions;
using SproutSocial.Service.HelperServices.Interfaces;
using SproutSocial.Service.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SproutSocial.Service.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthenticatedResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
                throw new AuthFailException("Username or Password invalid");

            if(!user.IsActive)
                throw new AuthFailException("User not active, please activate your account");

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user!.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = token.ValidTo.AddMinutes(3);
            
            await _userManager.UpdateAsync(user);

            return new AuthenticatedResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }

        public async Task<AuthenticatedResponseDto> RefreshTokenAsync(TokenPostDto tokenDto)
        {
            string? accessToken = tokenDto.AccessToken;
            string? refreshToken = tokenDto.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                throw new SecurityTokenException("Invalid access token or refresh token");

            string? userName = principal!.Identity!.Name;

            var user = await _userManager.FindByNameAsync(userName);
            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime > DateTime.UtcNow)
                throw new SecurityTokenException("Invalid access token or refresh token");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = newAccessToken.ValidTo.AddMinutes(3);

            await _userManager.UpdateAsync(user);

            return new AuthenticatedResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            };
        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var existUsername = await _userManager.FindByNameAsync(registerDto.Username);
            if (existUsername is not null)
                throw new RecordAlreadyExistException($"Username already exist! Username: {registerDto.Username}");

            var existEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if(existEmail is not null)
                throw new RecordAlreadyExistException($"Email already exist! Email: {registerDto.Email}");

            var user = new AppUser
            {
                Fullname = registerDto.Fullname,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                ProfilePhoto = registerDto.ProfilePhoto,
                IsActive = false
            };

            var identityResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!identityResult.Succeeded)
            {
                string result = String.Empty;
                foreach (var error in identityResult.Errors)
                {
                    result += error.Description;
                }
                throw new RegisterFailException(result);
            }

            await _userManager.AddToRoleAsync(user, RoleConstants.RoleType.Member.ToString());
        }

        public async Task RevokeAsync(string? userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new ItemNotFoundException("Invalid username");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = default;

            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByNameAsync(changePasswordDto.Username);
            if (user is null)
                throw new AuthFailException("Invalid username");

            if (!await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword))
                throw new AuthFailException("Old password is not valid.");

            var identityResult = await  _userManager
                .ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (!identityResult.Succeeded)
            {
                string result = String.Empty;
                foreach (var error in identityResult.Errors)
                {
                    result += error.Description;
                }
                throw new AuthFailException(result);
            }
        }
    }
}
