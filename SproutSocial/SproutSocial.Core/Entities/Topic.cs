using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core.Entities
{
    public class Topic : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<UserTopic> UserTopics { get; set; }
        public ICollection<BlogTopic> BlogTopics { get; set; }
    }
}
