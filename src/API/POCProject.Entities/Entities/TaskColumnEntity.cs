using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCProject.Entities.Entities;

/// <summary>
/// Represents a column for organizing tasks within a task management system.
/// </summary>
[Table(TableConstants.TaskColumnTableName)]
public class TaskColumnEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the task column. This name is used for identification and organization.
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the sort order for displaying columns.
    /// </summary>
    public int SortOrder { get; set; }
}
