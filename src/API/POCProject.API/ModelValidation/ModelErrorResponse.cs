using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace POCProject.API.ModelValidation;

/// <summary>
/// Represents a structured error response for model validation errors in an API.
/// It contains information such as the HTTP status code, a status phrase, a correlation ID,
/// a list of detailed errors, and a timestamp for when the error occurred.
/// </summary>
public class ModelErrorResponse
{
    /// <summary>
    /// Gets or sets the HTTP status code of the error response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the status phrase describing the error.
    /// </summary>
    public string? StatusPhrase { get; set; }

    /// <summary>
    /// Gets or sets a unique identifier for the request that caused the error,
    /// useful for tracking and troubleshooting.
    /// </summary>
    public Guid CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets a list of detailed error information related to the model validation.
    /// </summary>
    public List<ErrorDetails>? Errors { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the error response was generated.
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Generates an error response based on the provided action context.
    /// This method extracts validation errors from the model state and creates a <see cref="ModelErrorResponse"/> object.
    /// </summary>
    /// <param name="context">The action context containing the model state and other relevant information.</param>
    /// <returns>An <see cref="IActionResult"/> representing the error response.</returns>
    public static IActionResult GenerateErrorResponse(ActionContext context)
    {
        var apiError = new ModelErrorResponse
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            StatusPhrase = "Error",
            CorrelationId = Guid.NewGuid(), // Generate a new Guid for each error response
            TimeStamp = DateTime.UtcNow // Use UTC for consistency
        };

        apiError.Errors = context.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .Select(e => new ErrorDetails
            {
                Number = GetRowNumber(e.Key),
                Name = GetFieldName(e.Key),
                Message = e.Value!.Errors.First().ErrorMessage
            }).ToList();

        return new BadRequestObjectResult(apiError);
    }

    private static int GetRowNumber(string key)
    {
        if (key.Contains('.'))
            return Convert.ToInt32(key?.Split('.')[0]?.Trim('[', ']')) + 1;

        return 0;
    }

    private static string? GetFieldName(string key)
    {
        if (key.Contains('.'))
            return key?.Split('.')[1];

        return key;
    }
}

