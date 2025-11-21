using MindEase.Domain;

namespace MindEase.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<int> CreateUserAsync(User user);
        Task<bool> EmailExistsAsync(string email);
    }
}
