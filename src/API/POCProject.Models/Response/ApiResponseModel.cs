using System.Text.Json.Serialization;

namespace POCProject.Models.Response;

/// <summary>
/// Represents a standard response model for API responses.
/// It encapsulates the status code, success indication, response data,
/// an optional message, and a timestamp for the response creation.
/// </summary>
public class ApiResponseModel
{
    /// <summary>
    /// Gets or sets the HTTP status code of the response.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int StatusCode { get; set; }

    /// <summary>
    /// Indicates whether the API call was successful or not.
    /// </summary>
    public bool Response { get; set; }

    /// <summary>
    /// Contains any data returned by the API. Can be null if no data is returned.
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// Provides additional information about the response, such as error messages.
    /// Will be ignored in the JSON response if null.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>
    /// Records the timestamp of when the response was created.
    /// </summary>
    public DateTime? TimeStamp { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponseModel"/> class.
    /// Sets the <see cref="TimeStamp"/> to the current UTC time.
    /// </summary>
    public ApiResponseModel()
    {
        TimeStamp = DateTime.UtcNow;
    }
}


