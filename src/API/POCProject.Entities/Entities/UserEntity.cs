using POCProject.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POCProject.Entities.Entities;

[Table(TableConstants.UserTableName)]
public class UserEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the user's name.
    /// </summary>
    /// <remarks>
    /// This field is required. An error message will be returned if not provided.
    /// </remarks>
    [Required(ErrorMessage = "User name is required")]
    [StringLength(100)]
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    /// <remarks>
    /// This field is required. An error message will be returned if not provided.
    /// </remarks>
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100)]
    public string Password { get; set; }
}

