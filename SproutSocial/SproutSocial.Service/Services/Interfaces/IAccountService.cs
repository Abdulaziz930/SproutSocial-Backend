using SproutSocial.Service.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto registerDto);
    }
}
