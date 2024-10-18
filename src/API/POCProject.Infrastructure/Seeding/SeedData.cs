using POCProject.Entities.Entities;

namespace POCProject.Infrastructure.Seeding;

public class SeedData : ISeedData
{
    private readonly ApplicationDbContext _context;

    public SeedData(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SeedUsers()
    {
        if (_context.Users.Any())
        {
            return;
        }

        var user = new UserEntity
        {
            UserName = "admin",
            Password = "admin@123"
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
