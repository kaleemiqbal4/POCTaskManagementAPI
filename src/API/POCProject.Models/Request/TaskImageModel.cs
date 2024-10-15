namespace POCProject.Models.Request;

/// <summary> </summary>
public class TaskImageModel
{
    /// <summary>
    /// Gets or sets the foreign key referencing the associated task.
    /// </summary>
    public Guid TaskId { get; set; }

    /// <summary>
    /// Gets or sets the URL or path of the image.
    /// </summary>
    public string ImageUrl { get; set; }
}
