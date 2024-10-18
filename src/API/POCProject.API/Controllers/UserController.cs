using Microsoft.AspNetCore.Mvc;
using POCProject.Models.Request;
using POCProject.Services.Contract;

namespace POCProject.API.Controllers;

/// <summary>
/// Provides APIs for user management, including user creation and manipulation.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IUserService userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="_logger">An instance of <see cref="ILogger{UserController}"/> used for logging.</param>
    /// <param name="_userService"></param>
    public UserController(ILogger<UserController> _logger, IUserService _userService) => (logger, userService) = (_logger, _userService);

    /// <summary>
    /// Generate token of user with the specified username and password.
    /// </summary>
    /// <param name="userModel">The username of the user and password.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    public async Task<IActionResult> LoggedInUserAsync([FromBody] UserModel userModel)
    {
        var result = await userService.LoginAsync(userModel);
        return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
    }
}