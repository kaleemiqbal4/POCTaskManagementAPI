using Microsoft.AspNetCore.Mvc;
using POCProject.Models.Request;
using POCProject.Services.Contract;

namespace POCProject.API.Controllers;

/// <summary>
/// Controller for managing task columns in the task management application.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TaskColumnController : ControllerBase
{
    private readonly ITaskColumnService taskColumnService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskColumnController"/> class.
    /// </summary>
    /// <param name="_taskColumnService">The service to manage task columns.</param>
    public TaskColumnController(ITaskColumnService _taskColumnService) =>
        (taskColumnService) = (_taskColumnService);

    /// <summary>
    /// Creates a new column in the task board based on the provided <see cref="TaskColumnModel"/>.
    /// </summary>
    /// <param name="taskColumn">The model containing the details of the column to be created.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <response code="200">Returns the created column.</response>
    /// <response code="400">If the column creation fails due to invalid input.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TaskColumnModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateColumnAsync([FromBody] TaskColumnModel taskColumn, CancellationToken cancellationToken)
    {
        var response = await taskColumnService.AddColumnAsync(taskColumn, cancellationToken);
        return response.StatusCode == 201 ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Retrieves all columns from the task board.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A list of columns in the task board.</returns>
    /// <response code="200">Returns a list of task columns.</response>
    /// <response code="404">If no columns are found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskColumnModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetColumnsAsync(CancellationToken cancellationToken)
    {
        var response = await taskColumnService.GetColumnsAsync(cancellationToken);
        return response.StatusCode is 404 ? NotFound(response) : Ok(response);
    }
}