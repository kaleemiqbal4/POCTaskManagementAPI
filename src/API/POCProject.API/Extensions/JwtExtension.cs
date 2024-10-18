using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace POCProject.API.Extensions;

/// <summary>
/// Provides an extension method for configuring JWT authentication in a WebApplicationBuilder.
/// This class adds authentication services and sets up JWT bearer token validation parameters
/// using settings from the application's configuration file.
/// </summary>
public static class JwtExtension
{
    /// <summary>
    /// Configures JWT authentication services for the application.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder to configure.</param>
    public static void ConfigureJwt(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = jwtSettings["Key"]; // Retrieves the Key value
            var issuer = jwtSettings["Issuer"]; // Retrieves the Issuer value
            var audience = jwtSettings["Audience"]; // Retrieves the Audience value
            var duration = jwtSettings["DurationInMinutes"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
            };
            // Optional: Configure event handling for token validation failures
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 401;
                    var result = JsonSerializer.Serialize(new { message = "Authentication failed" });
                    return context.Response.WriteAsync(result);
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { message = "Authorization required" });
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { message = "Forbidden access: You do not have permission." });
                    return context.Response.WriteAsync(result);
                }
            };
        });
    }
}

