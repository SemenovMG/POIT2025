using Microsoft.IdentityModel.Tokens;
using POIT2025.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POIT2025.Services;

public class TokenService : ITokenService
{
    public const string SecretKey = "Super-S3cr3t!KeySuper-S3cr3t!KeySuper-S3cr3t!Key";

    public string CreateToken(UniversityUser user)
    {
        var payload = new List<Claim>() 
        {
            new("userId", user.Id.ToString()),
            new("test", "testValue"),
        };

        // TODO: add roles

        var authSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(SecretKey));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(2),
            claims: payload,
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
