using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fiscly.Api.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fiscly.Api.Auth;

public interface IJwtTokenService {
    string CreateToken(ApplicationUser user);
}

public sealed partial class JwtTokenService : IJwtTokenService {
    private readonly JwtOptions _options;
    private readonly ILogger<JwtTokenService> _logger;

    public JwtTokenService(IOptions<JwtOptions> options, ILogger<JwtTokenService> logger) {
        _options = options.Value;
        _logger = logger;
    }

    public string CreateToken(ApplicationUser user) {
        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FullName),
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _options.Issuer, audience: _options.Audience, claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);

        LogCreatedNewJwtTokenForFullname(user.Email);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [LoggerMessage(LogLevel.Information, "Created new Jwt token for {email}")]
    partial void LogCreatedNewJwtTokenForFullname(string email);
}