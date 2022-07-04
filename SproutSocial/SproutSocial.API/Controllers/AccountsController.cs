using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SproutSocial.Service.Dtos;
using SproutSocial.Service.Dtos.Account;
using SproutSocial.Service.Services.Interfaces;

namespace SproutSocial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _accountService.RegisterAsync(registerDto);

            return StatusCode(
                StatusCodes.Status201Created,
                new ResponseDto
                {
                    Status = StatusCodes.Status201Created,
                    Message = "User successfully registered"
                }
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _accountService.LoginAsync(loginDto));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenPostDto tokenPostDto)
        {
            return Ok(await _accountService.RefreshTokenAsync(tokenPostDto));
        }

        [Authorize]
        [HttpPost("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            await _accountService.RevokeAsync(username);

            return NoContent();
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            await _accountService.ChangePasswordAsync(changePasswordDto);

            return Ok(new ResponseDto
            {
                Status = StatusCodes.Status200OK,
                Message = "Password successfully changed"
            });
        }
    }
}
