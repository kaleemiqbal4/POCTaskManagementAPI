namespace POCProject.Models.Request;

/// <summary>
/// Represents the request to move a task from one column to another.
/// </summary>
public class MoveTaskModel
{
    /// <summary>
    /// Gets or sets the ID of the task to be moved.
    /// </summary>
    public int TaskId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the destination column to which the task will be moved.
    /// </summary>
    public int DestinationColumnId { get; set; }

    /// <summary>
    /// Gets or sets the optional new position of the task in the destination column.
    /// </summary>
    public int? NewPosition { get; set; }
}
