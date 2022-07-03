using SproutSocial.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Core
{
    public interface IUnitOfWork
    {
        ITopicRepository TopicRepository { get; }

        Task<int> CommitAsync();
    }
}
