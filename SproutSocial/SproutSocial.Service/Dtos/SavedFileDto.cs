using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos
{
    public class SavedFileDto
    {
        public string? FileName { get; set; }
        public string? ChangedFileName { get; set; }
        public string? Path { get; set; }
    }
}
