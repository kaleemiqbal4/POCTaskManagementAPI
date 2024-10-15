using AutoMapper;
using POCProject.Entities.Entities;
using POCProject.Infrastructure.Repositories.Concrete;
using POCProject.Infrastructure.Repositories.Contract;
using POCProject.Infrastructure.UnitOfWork;
using POCProject.Models.Request;
using POCProject.Models.Response;
using POCProject.Services.Contract;

namespace POCProject.Services.Concrete;

/// <summary>
/// Service for managing tasks.
/// </summary>
public class TasksService : ITasksService
{
    private readonly ITasksRepository tasksRepository;
    private readonly IWrapper wrapper;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TasksService"/> class.
    /// </summary>
    /// <param name="tasksRepository">The repository to manage task entities.</param>
    /// <param name="_wrapper">The unit of work implementation.</param>
    /// <param name="_mapper">Map Dtos and responses with entity vice versa</param>
    public TasksService(ITasksRepository _tasksRepository, IWrapper _wrapper, IMapper _mapper) =>
        (tasksRepository, wrapper, mapper) = (_tasksRepository, _wrapper, _mapper);

    /// <summary>
    /// Creates a new task based on the provided <see cref="TasksModel"/>.
    /// </summary>
    /// <param name="tasksModel">The model containing task details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> indicating the result of the operation.</returns>
    public async Task<BusinessResponse> CreateTaskAsync(TasksModel tasksModel, CancellationToken cancellationToken)
    {
        var tasksEntity = mapper.Map<TasksEntity>(tasksModel);
        await tasksRepository.CreateAsync(tasksEntity);
        return await wrapper.SaveChangesAsync(cancellationToken) > 0 ? new BusinessResponse(201, true, $"Task {tasksEntity.Name} has been inserted")
            : new BusinessResponse(404, true, $"Task {tasksEntity.Name} not inserted, try after some time");
    }

    /// <summary>
    /// Retrieves a list of tasks.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> containing the list of tasks.</returns>
    public async Task<BusinessResponse> GetTaskListAsync(CancellationToken cancellationToken)
    {
        var response = await tasksRepository.GetAllAsync().ConfigureAwait(false);
        return new BusinessResponse(200, true, response);
    }

    /// <summary>
    /// Retrieves a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> containing the task details if found; otherwise, an error response.</returns>
    public async Task<BusinessResponse> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await tasksRepository.GetByIdAsync(x => x.Id == id).ConfigureAwait(false);
        return response is null ? new BusinessResponse(404, false , "No record found") : new BusinessResponse(200, true, response);
    }
}
