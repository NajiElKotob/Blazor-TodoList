using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TodoListAPI.Endpoints;

/// <summary>
/// Contains extension methods for mapping Auth-related routes.
/// </summary>
public static class AuthEndpoints
{
    public record LoginRequest(string Username, string Password); // Moved outside the method

    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/login", async (HttpContext httpContext, LoginRequest loginRequest) =>
        {

            if (loginRequest.Username != "demo" || loginRequest.Password != "demo")
            {
                return Results.Unauthorized();
            }

            var key = Encoding.ASCII.GetBytes("YourSecretKeyHere");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Username.ToString()),
                    new Claim(ClaimTypes.Role, "DemoUser"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Results.Ok(tokenHandler.WriteToken(token));
        });
    }
}
