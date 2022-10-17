using Microsoft.EntityFrameworkCore.Design;

namespace SproutSocial.Persistence;

class DesignTimeDbContextFactory
{

}

//public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//{
//    private readonly AuditableEntitySaveChangesInterceptor? _interceptor;
//    public DesignTimeDbContextFactory()
//    {
//    }

//    public DesignTimeDbContextFactory(AuditableEntitySaveChangesInterceptor interceptor) : this()
//    {
//        _interceptor = interceptor;
//    }

//    public AppDbContext CreateDbContext(string[] args)
//    {
//        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
//        optionsBuilder.UseSqlServer(Configuration.ConnectionString);

//        return new(optionsBuilder.Options, _interceptor);
//    }
//}
