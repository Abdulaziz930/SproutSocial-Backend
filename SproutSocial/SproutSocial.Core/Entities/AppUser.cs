using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
        public string? ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserTopic> UserTopics { get; set; }
    }
}
