using POCProject.Entities.Entities;
using POCProject.Infrastructure.Repositories.Contract;

namespace POCProject.Infrastructure.Repositories.Concrete;

public class TaskColumnRepository(ApplicationDbContext dbContext) : BaseRepository<TaskColumnEntity>(dbContext), ITaskColumnRepository
{
}
