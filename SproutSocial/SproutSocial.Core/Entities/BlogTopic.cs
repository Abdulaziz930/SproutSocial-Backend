using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core.Entities
{
    public class BlogTopic
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}
