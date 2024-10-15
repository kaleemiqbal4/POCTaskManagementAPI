using POCProject.Models.Request;
using POCProject.Models.Response;

namespace POCProject.Services.Contract;

/// <summary>
/// Defines the operations for managing tasks within the application.
/// </summary>
public interface ITasksService
{
    /// <summary>
    /// Creates a new task based on the provided task model.
    /// </summary>
    /// <param name="tasksModel">The model containing the details of the task to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> indicating the result of the create operation.</returns>
    Task<BusinessResponse> CreateTaskAsync(TasksModel tasksModel, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of tasks.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> containing the list of tasks.</returns>
    Task<BusinessResponse> GetTaskListAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> containing the task details if found; otherwise, an error response.</returns>
    Task<BusinessResponse> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken);
}
