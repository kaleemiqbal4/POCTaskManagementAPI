using POCProject.Common.JwtHelper;
using POCProject.Infrastructure.Repositories.Contract;
using POCProject.Models.Request;
using POCProject.Models.Response;
using POCProject.Services.Contract;

namespace POCProject.Services.Concrete;

public class UserService : IUserService
{

    private readonly IUserRepository userRepository;
    private readonly IJwtTokenService jwtTokenService;

    public UserService(IUserRepository _userRepository, IJwtTokenService _jwtTokenService) => (userRepository, jwtTokenService) = (_userRepository, _jwtTokenService);

    public async Task<BusinessResponse> LoginAsync(UserModel user)
    {
        var result = string.Empty;
        var response = await userRepository.GetByIdAsync(x => x.UserName == user.UserName && user.Password == user.Password);
        return response is not null ?
           new BusinessResponse(200, true, jwtTokenService.GenerateJwtToken(response.UserName, response.Id)) :
           new BusinessResponse(404, false, "In valid username or password");
    }
}
