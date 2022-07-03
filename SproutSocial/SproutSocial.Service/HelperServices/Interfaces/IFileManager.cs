using Microsoft.AspNetCore.Http;
using SproutSocial.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.HelperServices.Interfaces
{
    public interface IFileManager
    {
        Task<SavedFileDto> SaveAsync(IFormFile file, string folder);
        Task<SavedFileDto> UpdateAsync(IFormFile file, string folder, string fileName);
    }
}
