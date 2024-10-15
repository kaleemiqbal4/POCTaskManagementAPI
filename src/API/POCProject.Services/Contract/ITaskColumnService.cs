using POCProject.Models.Request;
using POCProject.Models.Response;

namespace POCProject.Services.Contract;

public interface ITaskColumnService
{
    Task<BusinessResponse> AddColumnAsync(TaskColumnModel columnModel, CancellationToken cancellationToken);
    Task<BusinessResponse> GetColumnsAsync(CancellationToken cancellationToken);
}
