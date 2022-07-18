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
    public class BlogRepository : Repository<Blog, AppDbContext>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {
        }
    }
}
