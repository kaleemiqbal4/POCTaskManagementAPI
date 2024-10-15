using Microsoft.OpenApi.Models;
using System.Reflection;

namespace POCProject.API.Extensions;

/// <summary>Swagger Registration</summary>
public static class SwaggerRegistration
{
    /// <summary>
    /// Register Swagger
    /// </summary>
    /// <param name="builder"></param>
    public static void SetupSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            // Define the Swagger document with basic information
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "POC Project API",
                Version = "v1",
                Description = "This is the API documentation for the POC Project application. It provides endpoints for ... (add additional info as needed).",
                Contact = new OpenApiContact
                {
                    Name = "Support",
                    Email = "support@pocproject.com", // Update with actual support email
                    Url = new Uri("https://pocproject.com/support") // Update with actual support URL
                }
            });

            // Define the security scheme for Bearer token authentication
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter your Bearer token in the format **Bearer {token}**",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            // Add security requirements for the API
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
            });
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
