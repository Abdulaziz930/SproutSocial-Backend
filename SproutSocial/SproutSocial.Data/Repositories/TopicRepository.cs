using SproutSocial.Core.Entities;
using SproutSocial.Core.Repositories;
using SproutSocial.Data.Repositories.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Data.Repositories
{
    public class TopicRepository : Repository<Topic, AppDbContext>, ITopicRepository
    {
        public TopicRepository(AppDbContext context) : base(context)
        {
        }
    }
}
