namespace POCProject.Models.Response;

/// <summary>
/// Error Details
/// </summary>
public class ErrorDetail
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets any additional information about the error.
    /// </summary>
    public string Detail { get; set; }
}