using Microsoft.AspNetCore.Mvc;
using POCProject.Models.Request;
using POCProject.Models.Response;
using POCProject.Services.Contract;

namespace POCProject.API.Controllers;

/// <summary>
/// Handles HTTP requests related to task management, including creating, updating, deleting, 
/// and retrieving tasks. This controller provides endpoints for managing tasks within the 
/// application and supports operations like moving tasks between columns.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> logger;
    private readonly ITasksService tasksService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TasksController"/> class.
    /// </summary>
    /// <param name="_logger">The logger instance for logging information.</param>
    /// <param name="_tasksService">The user service instance for user-related operations.</param>
    public TasksController(ILogger<TasksController> _logger, ITasksService _tasksService) =>
        (logger, tasksService) = (_logger, _tasksService);

    /// <summary>
    /// Creates a new task based on the provided <see cref="TasksModel"/>.
    /// </summary>
    /// <param name="request">The model containing details of the task to be created.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TasksModel request, CancellationToken cancellationToken)
    {
        var response = await tasksService.CreateTaskAsync(request, cancellationToken);
        return response.StatusCode == 201 ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Retrieves All the tasks.
    /// </summary>
    /// <returns>An IActionResult containing the task details.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTaskListAsync(CancellationToken cancellationToken)
    {
        var response = await tasksService.GetTaskListAsync(cancellationToken);
        return response.StatusCode is 404 ? NotFound(response):  Ok(response);
    }

    /// <summary>
    /// Gets a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="BusinessResponse"/> containing the task details if found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await tasksService.GetTaskByIdAsync(id, cancellationToken);
        return response.StatusCode is 404 ? NotFound(response) : Ok(response);
    }
}


