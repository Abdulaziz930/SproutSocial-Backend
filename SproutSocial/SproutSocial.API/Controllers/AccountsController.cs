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

            return Ok(new ResponseDto { Status = StatusCodes.Status201Created, Message = "User successfully registered" });
        }
    }
}
