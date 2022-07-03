using SproutSocial.Core;
using SproutSocial.Core.Repositories;
using SproutSocial.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ITopicRepository _topicRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITopicRepository TopicRepository => _topicRepository = _topicRepository ?? new TopicRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
