namespace POCProject.Models.Response;

/// <summary>
/// Represents a business-specific response model that extends the base <see cref="ApiResponseModel"/>.
/// It provides additional constructors for creating response objects with different combinations of status, data, and messages.
/// </summary>
public class BusinessResponse : ApiResponseModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessResponse"/> class.
    /// This constructor can be used for creating a response without any parameters.
    /// </summary>
    public BusinessResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessResponse"/> class with specified status code and response indicator.
    /// </summary>
    /// <param name="statusCode">The HTTP status code of the response.</param>
    /// <param name="response">Indicates whether the API call was successful.</param>
    public BusinessResponse(int statusCode, bool response)
    {
        Response = response;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessResponse"/> class with specified status code, response indicator, and data.
    /// </summary>
    /// <param name="statusCode">The HTTP status code of the response.</param>
    /// <param name="response">Indicates whether the API call was successful.</param>
    /// <param name="data">The data returned by the API.</param>
    public BusinessResponse(int statusCode, bool response, object data) : this(statusCode, response)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessResponse"/> class with specified status code, response indicator, data, and message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code of the response.</param>
    /// <param name="response">Indicates whether the API call was successful.</param>
    /// <param name="data">The data returned by the API.</param>
    /// <param name="message">An optional message providing additional information about the response.</param>
    public BusinessResponse(int statusCode, bool response, object data, string message) : this(statusCode, response, data)
    {
        Message = message;
    }
}
