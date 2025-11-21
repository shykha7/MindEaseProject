

using Dapper;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Domain;
using System.Data;

namespace MindEase.Infrastructure.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly IDbConnection _db;
        public UserRepository(IDbConnection db) {
            _db = db; 
        }

        public async Task<int> CreateUserAsync(User user)
        {
            string query = @"
            INSERT INTO Users (FullName, Email, PasswordHash, DateOfBirth, UserType, CreatedAt, CreatedBy)
            VALUES (@FullName, @Email, @PasswordHash, @DateOfBirth, @UserType, @CreatedAt, @CreatedBy);

            SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var userId = await _db.ExecuteScalarAsync<int>(query, user);
            return userId;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            string query = "SELECT * FROM Users WHERE Email = @email";
            return await _db.QuerySingleOrDefaultAsync<User>(query, new { email });
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
            int count = await _db.ExecuteScalarAsync<int>(query, new { email });
            return count > 0;
        }
    }
}
