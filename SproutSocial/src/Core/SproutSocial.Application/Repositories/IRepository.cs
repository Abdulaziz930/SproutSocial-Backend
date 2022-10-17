using Microsoft.EntityFrameworkCore;
using SproutSocial.Domain.Entities.Common;

namespace SproutSocial.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
}
