using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Utils
{
    public static class Extensions
    {
        public static bool IsImage(this IFormFile formFile)
        {
            return formFile.ContentType.Contains("image/");
        }

        public static bool IsSizeAllowed(this IFormFile formFile, int kb)
        {
            return formFile.Length < kb * 1000;
        }
    }
}
