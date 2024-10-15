using POCProject.Entities.Entities;
using POCProject.Infrastructure.Repositories.Contract;

namespace POCProject.Infrastructure.Repositories.Concrete;

public class TasksRepository(ApplicationDbContext dbContext) : BaseRepository<TasksEntity>(dbContext), ITasksRepository
{
}
