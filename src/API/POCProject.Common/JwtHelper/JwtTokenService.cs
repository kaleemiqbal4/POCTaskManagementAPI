using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POCProject.Common.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POCProject.Common.JwtHelper;

/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWT) for user authentication.
/// This service reads JWT settings from the configuration and creates a token
/// containing the user's claims, which can be used for securing API endpoints.
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration containing JWT settings.</param>
    public JwtTokenService(IConfiguration configuration, JwtSettings jwtSettings)
    {
        _configuration = configuration;
        _jwtSettings = jwtSettings;
    }

    /// <summary>
    /// Generates a JWT token for the specified username.
    /// </summary>
    /// <param name="username">The username for which the token is generated.</param>
    /// <returns>A string representing the generated JWT token.</returns>
    public string GenerateJwtToken(string username, Guid id)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
           new Claim("id", id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.DurationInMinutes)),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
