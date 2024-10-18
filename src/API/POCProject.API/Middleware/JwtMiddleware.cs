using Microsoft.IdentityModel.Tokens;
using POCProject.Common.Common;
using POCProject.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace POCProject.API.Middleware;

/// <summary>
/// Middleware to handle JWT authentication and custom logic.
/// </summary>
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtMiddleware> _logger;
    private readonly JwtSettings _jwtSettings;

    public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger, JwtSettings jwtSettings)
    {
        _next = next;
        _logger = logger;
        _jwtSettings = jwtSettings;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var authHeader) && authHeader.ToString().StartsWith("Bearer "))
        {
            var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validate the token
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                context.User = principal;
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has expired.");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("Token validation failed: {Message}", ex.Message);
                var errorResponse = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Response = false
                };
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Error.Add(new ErrorDetail
                {
                    Message = $"Token has expired/invalid.",
                    Detail = "Please choose a valid token."
                });
                context.Response.WriteAsJsonAsync(errorResponse);
                return;
            }
        }


        // Call the next middleware in the pipeline
        await _next(context);
    }
}
