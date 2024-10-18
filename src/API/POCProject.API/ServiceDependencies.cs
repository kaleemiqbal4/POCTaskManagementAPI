using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCProject.API.Extensions;
using POCProject.API.Middleware;
using POCProject.API.ModelValidation;
using POCProject.Common.Common;
using POCProject.Infrastructure;
using POCProject.Services.DtoMapperProfile;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace POCProject.API;

/// <summary></summary>
public static class ServiceDependencies
{
    /// <summary>Register services</summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
    {
        #region "Bind JWT settings"
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        builder.Services.AddSingleton(jwtSettings);
        #endregion

        #region register PosgresSql Context
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("ConStr");
            options.UseSqlServer(connectionString);
        });
        #endregion
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        #region"Add Jwt"
        builder.ConfigureJwt();
        builder.Services.AddAuthorization();
        #endregion
        #region"AutoMapper"
        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        }, AppDomain.CurrentDomain.GetAssemblies());
        #endregion
        #region Model Validation"
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ModelErrorResponse.GenerateErrorResponse;
        });
        #endregion
        builder.ConfigureDependency();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.SetupSwagger();
        builder.ExtendCors();
        return builder;
    }

    /// <summary>Register http pipelines </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication ConfigurePipeLine(this WebApplication app)
    {
        app.UseMiddleware<JwtMiddleware>();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        #region Seed the database
        app.SeedDatabase();
        #endregion
        app.UseCors();
        return app;
    }
}
