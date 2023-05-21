using Google.Authenticator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SproutSocial.Application.Abstractions.Email;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Abstractions.Token;
using SproutSocial.Application.DTOs.MailDtos;
using SproutSocial.Application.DTOs.UserDtos;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Authentication.Token;
using SproutSocial.Application.Exceptions.Users;
using SproutSocial.Application.Helpers;
using SproutSocial.Application.Helpers.Extesions;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Domain.Enums;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace SproutSocial.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;
    private readonly IMailService _mailService;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
        , ITokenHandler tokenHandler, IUserService userService, IMailService mailService, IConfiguration configuration
        , IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _userService = userService;
        _mailService = mailService;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime)
    {
        var query = _userManager.Users.Include(u => u.UserTwoFaMethods)
            .ThenInclude(ut => ut.TwoFaMethod).AsQueryable();

        var user = await query.SingleOrDefaultAsync(u => u.UserName == model.UsernameOrEmail) 
            ?? await query.SingleOrDefaultAsync(u => u.Email == model.UsernameOrEmail);
        if (user == null)
            throw new AuthenticationFailException();

        if (!await _userManager.IsEmailConfirmedAsync(user))
            throw new EmailNotConfirmedException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (result.Succeeded)
        {
            if (user.TwoFactorEnabled)
            {
                TwoFactorAuthMethod twoFactorAuthMethod = TwoFactorAuthMethod.None;
                if(user.UserTwoFaMethods.Any(ut => ut.TwoFaMethod.TwoFactorAuthMethod == TwoFactorAuthMethod.Email && ut.IsSelected))
                {
                    string twoFaCode = await _userManager.GenerateTwoFactorTokenAsync(user, EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.Email));
                    await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "2FA Code", Body = $"Here is your code: {twoFaCode}" });
                    twoFactorAuthMethod = TwoFactorAuthMethod.Email;
                }
                else twoFactorAuthMethod = TwoFactorAuthMethod.GoogleAuthenticator;

                string twoFaSecurityToken = GenerateTwoFaUserSecurityToken(user.Id, _configuration["TwoFaSecretKey"]);

                return new() { RequiresTwoFactor = true, TwoFactorAuthMethod = twoFactorAuthMethod, TwoFaSecurityToken = twoFaSecurityToken };
            }

            var tokenResponse = await GenerateJwtTokenAsync(user, accessTokenLifeTime);
            return new()
            {
                RequiresTwoFactor = false,
                TokenResponse = tokenResponse,
                TwoFactorAuthMethod = TwoFactorAuthMethod.None
            };
        }

        throw new AuthenticationFailException();
    }

    public async Task<ConfirmEmailResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
    {
        confirmEmailDto.Token.ThrowIfNullOrWhiteSpace(message: "Token cannot be null");
        confirmEmailDto.Email.ThrowIfNullOrWhiteSpace(message: "Email cannot be null");

        var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {confirmEmailDto.Email}", HttpStatusCode.BadRequest);

        if (await _userManager.IsEmailConfirmedAsync(user))
            throw new EmailConfirmationException("This account already activated");

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
        if (!result.Succeeded)
            throw new EmailConfirmationException(result.Errors);

        return new(true, $"User successfully activated. Username: {user.UserName}");
    }

    public async Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user is null)
            throw new UserNotFoundException("User not found");

        if (user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            return await GenerateJwtTokenAsync(user, 2);
        }

        throw new RefreshTokenExpiredException();
    }

    public async Task<GetTwoFaSetupResponseDto> GetTwoFaSetupAsync(string email)
    {
        email.ThrowIfNullOrWhiteSpace(message: "Token cannot be null");

        var user = await _userManager.Users.Include(u => u.UserTwoFaMethods)
            .ThenInclude(ut => ut.TwoFaMethod)
            .SingleOrDefaultAsync(u => u.Email == email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {email}", HttpStatusCode.BadRequest);

        if (user.UserTwoFaMethods.Any(ut => ut.TwoFaMethod.TwoFactorAuthMethod == TwoFactorAuthMethod.Email))
            throw new TwoFaTypeAlreadyRegisteredException(TwoFactorAuthMethod.Email);

        string twoFaCode = await _userManager.GenerateTwoFactorTokenAsync(user, EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.Email));
        await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "2FA Code for enable 2FA", Body = $"Here is your code: {twoFaCode}" });

        return new(IsSuccess: true, Message: "2FA code sent to your email for enable 2FA");
    }

    public async Task EnableTwoFaAsync(EnableTwoFaDto enableTwoFaDto)
    {
        var user = await _userManager.Users.Include(u => u.UserTwoFaMethods)
            .SingleOrDefaultAsync(u => u.Email == enableTwoFaDto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {enableTwoFaDto.Email}", HttpStatusCode.BadRequest);

        bool isValidCode = await _userManager.VerifyTwoFactorTokenAsync(user, EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.Email), enableTwoFaDto.Code);
        if (!isValidCode)
            throw new AuthenticationFailException("Invalid Code");

        if(!user.TwoFactorEnabled)
            await _userManager.SetTwoFactorEnabledAsync(user, true);

        var twoFaMethod = await _unitOfWork.TwoFaMethodReadRepository.GetSingleAsync(tfa => tfa.TwoFactorAuthMethod == TwoFactorAuthMethod.Email);

        UserTwoFaMethod userTwoFaMethod = new()
        {
            UserId = user.Id,
            TwoFaMethodId = twoFaMethod.Id
        };

        bool isSelected = user.UserTwoFaMethods.Any(ut => ut.IsSelected);
        if (!isSelected)
        {
            userTwoFaMethod.IsSelected = true;
        }

        user.UserTwoFaMethods.Add(userTwoFaMethod);
        await _userManager.UpdateAsync(user);
    }

    public async Task<TokenResponseDto> TwoFaLoginAsync(TwoFaLoginDto twoFaLoginDto, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByEmailAsync(twoFaLoginDto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {twoFaLoginDto.Email}", HttpStatusCode.BadRequest);

        bool isValidToken = VerifyTwoFaUserSecurityToken(user.Id, twoFaLoginDto.TwoFaSecurityToken, _configuration["TwoFaSecretKey"]);
        if(!isValidToken)
            throw new AuthenticationFailException("Please enter username/email and password");

        bool isValidCode = await _userManager.VerifyTwoFactorTokenAsync(user, EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.Email), twoFaLoginDto.Code);
        if (!isValidCode)
            throw new AuthenticationFailException("Invalid Code");

        return await GenerateJwtTokenAsync(user, accessTokenLifeTime);
    }

    public async Task<byte[]> GetGAuthSetupAsync(string email)
    {
        var user = await _userManager.Users.Include(u => u.UserTwoFaMethods)
            .ThenInclude(ut => ut.TwoFaMethod)
            .SingleOrDefaultAsync(u => u.Email == email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {email}", HttpStatusCode.BadRequest);

        if (user.UserTwoFaMethods.Any(ut => ut.TwoFaMethod.TwoFactorAuthMethod == TwoFactorAuthMethod.GoogleAuthenticator))
            throw new TwoFaTypeAlreadyRegisteredException(TwoFactorAuthMethod.GoogleAuthenticator);

        string tokenProviderName = EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.GoogleAuthenticator);
        var authenticatorKey = await _userManager.GetAuthenticationTokenAsync(user, tokenProviderName, "2FA");
        if (authenticatorKey == null)
        {
            authenticatorKey = await _userManager.GenerateUserTokenAsync(user, tokenProviderName, "2FA");
            await _userManager.SetAuthenticationTokenAsync(user, tokenProviderName, "2FA", authenticatorKey);
        }

        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

        var setupInfo = tfa.GenerateSetupCode("SproutSocial", user.Email, authenticatorKey, false, 3);
        byte[] imageData = Convert.FromBase64String(setupInfo.QrCodeSetupImageUrl.Split(',')[1]);

        return imageData;
    }

    public async Task EnableGAuthAsync(SetGAuthDto setGAuthDto)
    {
        var user = await _userManager.Users.Include(u => u.UserTwoFaMethods)
            .SingleOrDefaultAsync(u => u.Email == setGAuthDto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {setGAuthDto.Email}", HttpStatusCode.BadRequest);

        var authenticatorKey = await _userManager.GetAuthenticationTokenAsync(user, EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.GoogleAuthenticator), "2FA"); ;
        if (authenticatorKey is null)
            throw new UserNotRegisteredGAuthException();

        var authenticator = new TwoFactorAuthenticator();

        var isValidCode = authenticator.ValidateTwoFactorPIN(authenticatorKey, setGAuthDto.Code);
        if (!isValidCode)
            throw new AuthenticationFailException("Invalid Code");

        if (!user.TwoFactorEnabled)
            await _userManager.SetTwoFactorEnabledAsync(user, true);

        var twoFaMethod = await _unitOfWork.TwoFaMethodReadRepository.GetSingleAsync(tfa => tfa.TwoFactorAuthMethod == TwoFactorAuthMethod.GoogleAuthenticator);

        UserTwoFaMethod userTwoFaMethod = new()
        {
            UserId = user.Id,
            TwoFaMethodId = twoFaMethod.Id
        };

        bool isSelected = user.UserTwoFaMethods.Any(ut => ut.IsSelected);
        if (!isSelected)
        {
            userTwoFaMethod.IsSelected = true;
        }

        user.UserTwoFaMethods.Add(userTwoFaMethod);
        await _userManager.UpdateAsync(user);
    }

    public async Task<TokenResponseDto> GAuthLoginAsync(TwoFaLoginDto twoFaLoginDto, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByEmailAsync(twoFaLoginDto.Email);
        if (user is null)
            throw new UserNotFoundException($"User not found by email: {twoFaLoginDto.Email}", HttpStatusCode.BadRequest);

        bool isValidToken = VerifyTwoFaUserSecurityToken(user.Id, twoFaLoginDto.TwoFaSecurityToken, _configuration["TwoFaSecretKey"]);
        if (!isValidToken)
            throw new AuthenticationFailException("Please enter username/email and password");

        var authenticatorKey = await _userManager.GetAuthenticationTokenAsync(user, EnumHelper.GetEnumDisplayName(TwoFactorAuthMethod.GoogleAuthenticator), "2FA");
        if (authenticatorKey is null)
            throw new UserNotRegisteredGAuthException();

        var authenticator = new TwoFactorAuthenticator();

        var isValidCode = authenticator.ValidateTwoFactorPIN(authenticatorKey, twoFaLoginDto.Code);
        if (!isValidCode)
            throw new AuthenticationFailException("Invalid Code");

        return await GenerateJwtTokenAsync(user, accessTokenLifeTime);
    }

    private string GenerateTwoFaUserSecurityToken(Guid userId, string secretKey)
    {
        string data = userId.ToString();
        byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        using (var hmac = new HMACSHA256(keyBytes))
        {
            byte[] hashBytes = hmac.ComputeHash(dataBytes);
            string token = Convert.ToBase64String(hashBytes);
            return token;
        }
    }

    private bool VerifyTwoFaUserSecurityToken(Guid userId, string token, string secretKey)
    {
        string expectedToken = GenerateTwoFaUserSecurityToken(userId, secretKey);
        return token.Equals(expectedToken);
    }

    private async Task<TokenResponseDto> GenerateJwtTokenAsync(AppUser user, int accessTokenLifeTime)
    {
        var tokenResponse = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
        await _userService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.Expiration, 2);
        return tokenResponse;
    }
}