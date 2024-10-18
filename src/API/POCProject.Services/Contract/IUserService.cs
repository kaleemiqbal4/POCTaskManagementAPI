using POCProject.Models.Request;
using POCProject.Models.Response;

namespace POCProject.Services.Contract;

public interface IUserService
{
    Task<BusinessResponse> LoginAsync(UserModel user);
}
