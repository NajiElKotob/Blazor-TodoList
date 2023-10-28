using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoListAPI.Endpoints;
using TodoListAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoListConnectionString")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
/*            builder.WithOrigins("http://localhost:5099", "https://example.net")
                    .AllowAnyHeader()
                   .AllowAnyMethod();*/

            // Allow all origins
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Register authentication services to the dependency injection container. 
// This alone does not enforce authentication. Additional configuration and middleware 
// are required to protect routes and endpoints.

// Requires Microsoft.AspNetCore.Authentication.JwtBearer
// Add support for JWT (JSON Web Token) bearer authentication.
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization();

/*builder.Services.AddAuthorizationBuilder()
        .AddPolicy("DefaultAuthPolicy", policy =>
        policy
        .RequireRole("admin")
        .RequireClaim("department", "IT"));*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

// ----------------- Start of Endpoints -----------------


// Map the endpoints to the web application.
app.MapTodoEndpoints();
app.MapAuthEndpoints();

// ----------------- End of Endpoints -----------------

app.Run();

