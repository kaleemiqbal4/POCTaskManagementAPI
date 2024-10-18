using POCProject.Entities.Entities;
using POCProject.Infrastructure.Repositories.Contract;

namespace POCProject.Infrastructure.Repositories.Concrete;

public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<UserEntity>(dbContext), IUserRepository
{
}
