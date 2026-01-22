using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameStoreAPI.Common.Helpers;

public static class Token
{
    public static string GenerateAccessToken(Guid userId, IConfiguration config)
    {
        var keyInput = config["Jwt:Key"] ?? "ThisIsMySuperSecretKey12345678!!!";
        var key = new SymmetricSecurityKey(Convert.FromBase64String(keyInput));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        Console.WriteLine(config["Jwt:Key"]);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60), // Set expiration
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
    
    public static string GenerateRefreshToken(Guid userId, IConfiguration config)
    {
        var keyInput = config["Jwt:Key"] ?? "ThisIsMySuperSecretKey12345678!!!";
        var key = new SymmetricSecurityKey(Convert.FromBase64String(keyInput));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60), // Set expiration
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}