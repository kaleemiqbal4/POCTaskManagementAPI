namespace POCProject.Common.JwtHelper;

/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWT) for user authentication.
/// This service reads JWT settings from the configuration and creates a token
/// containing the user's claims, which can be used for securing API endpoints.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the specified username.
    /// </summary>
    /// <param name="username">The username for which the token is generated.</param>
    /// <returns>A string representing the generated JWT token.</returns>
    string GenerateJwtToken(string username, Guid id);
}
