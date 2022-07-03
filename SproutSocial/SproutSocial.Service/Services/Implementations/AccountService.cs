using Microsoft.AspNetCore.Identity;
using SproutSocial.Core.Entities;
using SproutSocial.Data.Identity;
using SproutSocial.Service.Dtos.Account;
using SproutSocial.Service.Exceptions;
using SproutSocial.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}
