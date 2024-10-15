using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCProject.Entities.Entities;

/// <summary> </summary>
[Table(TableConstants.TaskImageTableName)]
public class TaskImageEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key referencing the associated task.
    /// </summary>
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the URL or path of the image.
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Navigation property (optional)
    /// </summary>
    [ForeignKey(nameof(TaskId))]
    public virtual TasksEntity Tasks { get; set; }
}
