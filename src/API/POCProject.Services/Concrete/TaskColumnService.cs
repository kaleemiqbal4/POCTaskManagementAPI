using AutoMapper;
using POCProject.Entities.Entities;
using POCProject.Infrastructure.Repositories.Contract;
using POCProject.Infrastructure.UnitOfWork;
using POCProject.Models.Request;
using POCProject.Models.Response;
using POCProject.Services.Contract;

namespace POCProject.Services.Concrete;

/// <summary>
/// Service for managing task columns in the task management application.
/// </summary>
public class TaskColumnService : ITaskColumnService
{
    private readonly ITaskColumnRepository taskColumnRepository;
    private readonly IWrapper wrapper;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskColumnService"/> class.
    /// </summary>
    /// <param name="_taskColumnRepository">The repository to manage task columns.</param>
    /// <param name="_wrapper">The unit of work implementation for managing transactions.</param>
    /// <param name="_mapper">The AutoMapper instance used for mapping between models and entities.</param>
    public TaskColumnService(ITaskColumnRepository _taskColumnRepository, IWrapper _wrapper, IMapper _mapper) =>
        (taskColumnRepository, wrapper, mapper) = (_taskColumnRepository, _wrapper, _mapper);

    /// <summary>
    /// Adds a new column to the task board based on the provided <see cref="TaskColumnModel"/>.
    /// </summary>
    /// <param name="columnModel">The model containing the details of the column to be added.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> indicating the result of the operation.</returns>
    public async Task<BusinessResponse> AddColumnAsync(TaskColumnModel columnModel, CancellationToken cancellationToken)
    {
        var taskColumnEntity = mapper.Map<TaskColumnEntity>(columnModel);

        await taskColumnRepository.CreateAsync(taskColumnEntity);
        return await wrapper.SaveChangesAsync(cancellationToken) > 0 ?  new BusinessResponse(201, true, $"Task {taskColumnEntity.Name} has been inserted") : new BusinessResponse(404, true, $"Task {taskColumnEntity.Name} not inserted, try after some time");

    }

    /// <summary>
    /// Retrieves all columns from the task board.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> containing the list of columns.</returns>
    public async Task<BusinessResponse> GetColumnsAsync(CancellationToken cancellationToken)
    {
        var response = await taskColumnRepository.GetAllAsync().ConfigureAwait(false);
        return new BusinessResponse(200, true, response);
    }
}

