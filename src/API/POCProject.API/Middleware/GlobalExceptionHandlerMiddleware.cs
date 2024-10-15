using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POCProject.Models.Response;
using System.Text.RegularExpressions;

namespace POCProject.API.Middleware;

/// <summary>
/// Middleware for globally handling exceptions in the application.
/// Catches database update exceptions and other general exceptions,
/// providing user-friendly error messages.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware and handles any exceptions that occur during processing.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            await HandleDbUpdateExceptionAsync(context, sqlEx);
        }
        catch (Exception ex)
        {
            await HandleGeneralExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles database update exceptions, specifically for duplicate key errors.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <param name="sqlEx">The SQL exception thrown during the database operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static Task HandleDbUpdateExceptionAsync(HttpContext context, SqlException sqlEx)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status409Conflict,
            Response = false
        };

        if (sqlEx.Number == 2601) // SQL error code for duplicate key
        {
            var duplicateKeyValue = ExtractDuplicateKeyValue(sqlEx.Message);
            errorResponse.Error.Add(new ErrorDetail
            {
                Message = $"A entry column with the name '{duplicateKeyValue}' already exists.",
                Detail = "Please choose a different name."
            });
            context.Response.StatusCode = StatusCodes.Status409Conflict;
        }
        else
        {
            errorResponse.Error.Add(new ErrorDetail
            {
                Message = "An unexpected database error occurred.",
                Detail = sqlEx.Message
            });
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        return context.Response.WriteAsJsonAsync(errorResponse);
    }

    /// <summary>
    /// Handles general exceptions that occur during the request processing.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <param name="ex">The exception that was thrown.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Response = false
        };

        errorResponse.Error.Add(new ErrorDetail
        {
            Message = "An unexpected error occurred.",
            Detail = ex.Message
        });

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsJsonAsync(errorResponse);
    }

    /// <summary>
    /// Extracts the duplicate key value from the SQL exception message.
    /// </summary>
    /// <param name="message">The message from the SQL exception.</param>
    /// <returns>The duplicate key value or "unknown value" if not found.</returns>
    private static string ExtractDuplicateKeyValue(string message)
    {
        var match = Regex.Match(message, @"duplicate key value is \(([^)]+)\)");
        return match.Success ? match.Groups[1].Value : "unknown value";
    }
}
