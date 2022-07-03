using Microsoft.AspNetCore.Http;
using SproutSocial.Service.Dtos;
using SproutSocial.Service.HelperServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.HelperServices.Implementations
{
    public class FileManager : IFileManager
    {
        private readonly IHelperAccessor _helperAccessor;

        public FileManager(IHelperAccessor helperAccessor)
        {
            _helperAccessor = helperAccessor;
        }

        public async Task<SavedFileDto> SaveAsync(IFormFile file, string folder)
        {
            var newFileName = $"{Guid.NewGuid()}-{file.FileName}";
            string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/uploads/", folder, newFileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            SavedFileDto savedFile = new SavedFileDto
            {
                FileName = file.FileName,
                ChangedFileName = newFileName,
                Path = $"{_helperAccessor.BaseUrl}/uploads/{folder}/{newFileName}"
            };
            return savedFile;
        }

        public async Task<SavedFileDto> UpdateAsync(IFormFile file, string folder, string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/uploads/", folder, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SavedFileDto savedFileDto = await SaveAsync(file, folder);

            return savedFileDto;
        }
    }
}
