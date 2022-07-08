using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public ICollection<BlogTopic> BlogTopics { get; set; }
    }
}
