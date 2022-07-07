using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SproutSocial.Core;
using SproutSocial.Core.Entities;
using SproutSocial.Data.Identity;
using SproutSocial.Service.Dtos.Account;
using SproutSocial.Service.Dtos.UserTopicDtos;
using SproutSocial.Service.Exceptions;
using SproutSocial.Service.HelperServices.Interfaces;
using SproutSocial.Service.Services.Interfaces;
using SproutSocial.Service.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SproutSocial.Service.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user is null)
                throw new AuthFailException("Invalid email address");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string message = $"<a href='{token}'>Reset Password</a>";

            await EmailSender.SendEmailAsync(forgotPasswordDto.Email, message, "SproutSocial - Reset Password");
        }

        public async Task ResetPasswordAsync(string id, ResetPasswordDto resetPasswordDto)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id cannot be null");

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new ItemNotFoundException("user not found");

            var identityResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
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

        public async Task SelectTopicAsync(string userId, int? topicId)
        {
            if (string.IsNullOrWhiteSpace(userId) || topicId is null)
                throw new ArgumentNullException("id cannot be null");

            var isExistTopic = await _unitOfWork.TopicRepository.IsExistsAsync(x => x.Id == topicId);
            if (!isExistTopic)
                throw new ItemNotFoundException($"Topic not found by id: {topicId}");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new AuthFailException("User not found");

            UserTopicDto userTopicDto = new UserTopicDto
            {
                UserId = userId,
                TopicId = topicId.Value
            };

            List<UserTopic> userTopics = new List<UserTopic>()
            {
                 _mapper.Map<UserTopic>(userTopicDto)
            };

            user.UserTopics = userTopics;

            await _unitOfWork.CommitAsync();
        }
    }
}
