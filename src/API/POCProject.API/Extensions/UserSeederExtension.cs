using POCProject.Infrastructure.Seeding;

namespace POCProject.API.Extensions;

/// <summary>
/// 
/// </summary>
public static class UserSeederExtension
{
    public static void SeedDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var seedDataLogic = scope.ServiceProvider.GetRequiredService<ISeedData>();
            seedDataLogic.SeedUsers();
        }
    }
}
