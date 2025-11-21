
using MindEase.Application.DTOs.Client;

namespace MindEase.Application.Interfaces.Services
{
    public interface IClientService
    {
        Task<int> CreateClientAsync(CreateClientDto dto, int createdByUserId);
        Task<ClientResponseDto?> GetClientByIdAsync(int clientId);
        Task<ClientResponseDto?> GetClientByUserIdAsync(int userId);
        Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync();
        Task<bool> UpdateClientAsync(int clientId, UpdateClientDto dto);
        Task<bool> DeleteClientAsync(int clientId);
    }
}
