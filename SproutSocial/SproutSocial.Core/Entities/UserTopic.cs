using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core.Entities
{
    public class UserTopic
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
