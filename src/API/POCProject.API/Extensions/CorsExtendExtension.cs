namespace POCProject.API.Extensions;

/// <summary>
/// Provides extension methods for configuring Cross-Origin Resource Sharing (CORS) in a web application.
/// This class allows you to easily add and configure CORS policies for the application.
/// </summary>
public static class CorsExtendExtension
{
    /// <summary>
    /// Configures the default CORS policy for the application.
    /// Allows any origin, any header, and any HTTP method.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance to which the CORS configuration will be applied.</param>
    public static void ExtendCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(corsBuilder =>
            {
                corsBuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}

