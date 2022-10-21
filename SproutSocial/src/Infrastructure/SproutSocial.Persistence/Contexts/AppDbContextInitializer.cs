using Microsoft.AspNetCore.Identity;
using SproutSocial.Domain.Entities.Identity;
using SproutSocial.Persistence.Enums;

namespace SproutSocial.Persistence.Contexts;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public AppDbContextInitializer(AppDbContext context, RoleManager<IdentityRole<Guid>> roleManager, UserManager<AppUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            //TODO: Loging
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception)
        {
            //TODO: Loging
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default Roles
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            if (_roleManager.Roles.All(r => r.Name != role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = role.ToString()});
            }
        }

        // Default Users

        var admin = new AppUser { UserName = "admin@localhost", Email = "admin@localhost.com" };

        if (_userManager.Users.All(u => u.UserName != admin.UserName))
        {
            await _userManager.CreateAsync(admin, "Pa$$word1");
            //await _userManager.AddToRoleAsync(administrator, administratorRole.Name );
            await _userManager.AddToRolesAsync(admin, new[] { Roles.Admin.ToString() });
        }
    }
}
