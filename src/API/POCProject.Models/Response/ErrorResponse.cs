namespace POCProject.Models.Response;



public class ErrorResponse
{
    /// <summary>
    /// Gets or sets the HTTP status code of the response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Indicates whether the response is successful.
    /// </summary>
    public bool Response { get; set; }

    /// <summary>
    /// Contains error details related to the response.
    /// </summary>
    public List<ErrorDetail> Error { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the error response.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
    /// </summary>
    public ErrorResponse()
    {
        Error = new List<ErrorDetail>();
        TimeStamp = DateTime.UtcNow; // Default to current UTC time
    }
}