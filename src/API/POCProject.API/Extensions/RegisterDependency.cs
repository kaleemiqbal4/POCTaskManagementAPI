using POCProject.Infrastructure.Repositories.Concrete;
using POCProject.Infrastructure.Repositories.Contract;
using POCProject.Infrastructure.UnitOfWork;
using POCProject.Services.Concrete;
using POCProject.Services.Contract;

namespace POCProject.API.Extensions;

/// <summary> </summary>
public static class RegisterDependency
{
    /// <summary></summary>
    /// <param name="builder"></param>
    public static void ConfigureDependency(this WebApplicationBuilder builder)
    {
        #region "Register Repository"
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<ITasksRepository, TasksRepository>();
        builder.Services.AddScoped<ITaskImageRepository, TaskImageRepository>();
        builder.Services.AddScoped<ITaskColumnRepository, TaskColumnRepository>();
        builder.Services.AddScoped<IWrapper, Wrapper>();
        #endregion

        #region "Register Services"
        builder.Services.AddScoped<ITasksService, TasksService>();
        builder.Services.AddScoped<ITaskColumnService, TaskColumnService>();
        builder.Services.AddScoped<ITaskImageService, TaskImageService>();
        #endregion
    }
}
