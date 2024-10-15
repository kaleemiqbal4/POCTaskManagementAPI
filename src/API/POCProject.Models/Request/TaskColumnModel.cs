using System.ComponentModel.DataAnnotations;

namespace POCProject.Models.Request;

/// <summary>
/// Represents a column for organizing tasks within a task management system.
/// </summary>

public class TaskColumnModel
{
    /// <summary>
    /// Gets or sets the name of the task column. This name is used for identification and organization.
    /// </summary>
    [StringLength(100, ErrorMessage = "Task Column Name can't exceed than 100 chars")]
    [Required(ErrorMessage ="Task Column Name is required")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the sort order for displaying columns.
    /// </summary>
    public int SortOrder { get; set; }
}
