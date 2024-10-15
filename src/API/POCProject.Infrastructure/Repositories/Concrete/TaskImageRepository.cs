using POCProject.Entities.Entities;
using POCProject.Infrastructure.Repositories.Contract;

namespace POCProject.Infrastructure.Repositories.Concrete;

public class TaskImageRepository(ApplicationDbContext dbContext) : BaseRepository<TaskImageEntity>(dbContext), ITaskImageRepository
{
}
