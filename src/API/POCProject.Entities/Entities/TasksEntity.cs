using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCProject.Entities.Entities;

/// <summary>
/// Represents a task entity in the task management system.
/// </summary>
[Table(TableConstants.TaskTableName)]
public class TasksEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the task.
    /// The name must be 100 characters or less.
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// The description can be up to 500 characters long.
    /// </summary>
    [StringLength(500)]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the deadline for the task.
    /// This property can be null if no deadline is set.
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is marked as favorited.
    /// </summary>
    public bool IsFavorited { get; set; }

    /// <summary>
    /// Gets or sets the foreign key referencing the associated task column.
    /// </summary>
    public Guid TaskColumnId { get; set; }

    /// <summary>
    /// Navigation property to the associated task column.
    /// </summary>
    [ForeignKey(nameof(TaskColumnId))]
    public virtual TaskColumnEntity Column { get; set; }
}

