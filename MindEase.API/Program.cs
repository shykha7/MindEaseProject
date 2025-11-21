using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Application.Interfaces.Services;
using MindEase.Infrastructure.Repositories;
using MindEase.Infrastructure.Services;
using System.Data;
using System.Security.Claims;
using System.Text;
using MindEase.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Register Database Connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Repositories
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ITherapistRepository,TherapistRepository>();
builder.Services.AddScoped<IClientRepository,ClientRepository>();


//Add Services
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ITherapistService,TherapistService>();
builder.Services.AddScoped<IClientService,ClientService>();

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            RoleClaimType = ClaimTypes.Role // Make sure this matches your JWT token claims
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
