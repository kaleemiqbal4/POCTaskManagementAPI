using System.ComponentModel.DataAnnotations;

namespace POCProject.Models.Request;

/// <summary>
/// Represents a task model used for task management.
/// </summary>
public class TasksModel
{
    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    [Required(ErrorMessage ="Task name is required")]
    [StringLength(255, ErrorMessage ="Task name length should not be exceed than 255 chars")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    [Required(ErrorMessage = "Task description is required")]
    [StringLength(500, ErrorMessage = "Task description length should not be exceed than 500 chars")]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the deadline for the task.
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// Gets or sets the ID of the column to which the task belongs.
    /// </summary>
    public int ColumnId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is favorited.
    /// </summary>
    public bool IsFavorited { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the task's column.
    /// </summary>
    [Required(ErrorMessage ="Task column Id is required")]
    public Guid TaskColumnId { get; set; }
}
