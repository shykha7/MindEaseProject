using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using MindEase.Application.DTOs.Auth;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Application.Interfaces.Services;
using MindEase.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace MindEase.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if email already exists
            request.Email = request.Email.ToLower().Trim();
            if (await _repo.EmailExistsAsync(request.Email))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Map DTO → Domain Entity
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                DateOfBirth = request.DateOfBirth,
                UserType = request.UserType,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = null
            };

            int newUserId = await _repo.CreateUserAsync(user);

            return new AuthResponse
            {
                Success = true,
                Message = "User registered successfully",
                UserID = newUserId
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
    {
                 new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, user.UserType), // ADMIN or USER
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            request.Email = request.Email.ToLower().Trim();
            var user = await _repo.GetUserByEmailAsync(request.Email);
           

            if (user == null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            // Validate password
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValid)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            string token = GenerateJwtToken(user);
            return new AuthResponse
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                UserID = user.UserID
            };
        }


    }
}
