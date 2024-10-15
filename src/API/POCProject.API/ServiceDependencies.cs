using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCProject.API.Extensions;
using POCProject.API.Middleware;
using POCProject.API.ModelValidation;
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
       // GetConnectionFromKeyValut();
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
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors();
        return app;
    }
}
