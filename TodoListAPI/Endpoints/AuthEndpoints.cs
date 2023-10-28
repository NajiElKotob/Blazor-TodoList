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


    // Record representing login request data.
    public record LoginRequest(string Username, string Password);

    /// <summary>
    /// Maps authentication-related endpoints.
    /// </summary>
    /// <param name="app">The web application builder.</param>
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var jwtSettings = app.Configuration.GetSection("JwtSettings");


        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        // Endpoint for user login.
        app.MapPost("/login", async (HttpContext httpContext, LoginRequest loginRequest) =>
        {
            // Dummy user validation. Replace with actual user validation logic.
            if (loginRequest.Username != "demo" || loginRequest.Password != "demo")
            {
                return Results.Unauthorized();  // Return 401 Unauthorized response.
            }

            // Token handler for creating JWT tokens.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Define token properties.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Username.ToString()),
                    new Claim(ClaimTypes.Role, "DemoUser"),
                }),
                Issuer = jwtSettings["Issuer"],  
                Audience = jwtSettings["Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),  // Token expiration time.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create a new token.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the generated token.
            return Results.Ok(tokenHandler.WriteToken(token));
        });
    }
}
