

using MindEase.Domain;

namespace MindEase.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<int> CreateClientAsync(Client client);
        Task<Client?> GetClientByIdAsync(int clientId);
        Task<Client?> GetClientByUserIdAsync(int userId);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> DeleteClientAsync(int clientId);
    }
}
