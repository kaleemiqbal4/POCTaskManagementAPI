using System.ComponentModel.DataAnnotations;

namespace POCProject.Models.Request;

/// <summary>
/// Represents a user in the system with essential credentials.
/// </summary>
public class UserModel
{
    /// <summary>
    /// Gets or sets the user's name.
    /// </summary>
    /// <remarks>
    /// This field is required. An error message will be returned if not provided.
    /// </remarks>
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    /// <remarks>
    /// This field is required. An error message will be returned if not provided.
    /// </remarks>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}