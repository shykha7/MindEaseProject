

using MindEase.Application.DTOs.Client;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Application.Interfaces.Services;
using MindEase.Domain;

namespace MindEase.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> CreateClientAsync(CreateClientDto dto, int createdByUserId)
        {
            var client = new Client
            {
                UserID = createdByUserId,
                EmergencyContactName = dto.EmergencyContactName,
                EmergencyContactPhone = dto.EmergencyContactPhone,
                IsReadyToStart = dto.IsReadyToStart,
                HasCompletedPayment = dto.HasCompletedPayment,
                CreatedBy = createdByUserId
            };

            return await _clientRepository.CreateClientAsync(client);
        }

        public async Task<ClientResponseDto?> GetClientByIdAsync(int clientId)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);
            if (client == null) return null;

            return MapToResponseDto(client);
        }

        public async Task<ClientResponseDto?> GetClientByUserIdAsync(int userId)
        {
            var client = await _clientRepository.GetClientByUserIdAsync(userId);
            if (client == null) return null;

            return MapToResponseDto(client);
        }

        public async Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return clients.Select(c => MapToResponseDto(c));
        }

        public async Task<bool> UpdateClientAsync(int clientId, UpdateClientDto dto)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);
            if (client == null) return false;

            client.EmergencyContactName = dto.EmergencyContactName ?? client.EmergencyContactName;
            client.EmergencyContactPhone = dto.EmergencyContactPhone ?? client.EmergencyContactPhone;
            client.IsReadyToStart = dto.IsReadyToStart ?? client.IsReadyToStart;
            client.HasCompletedPayment = dto.HasCompletedPayment ?? client.HasCompletedPayment;

            return await _clientRepository.UpdateClientAsync(client);
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            return await _clientRepository.DeleteClientAsync(clientId);
        }

        private ClientResponseDto MapToResponseDto(Client client)
        {
            return new ClientResponseDto
            {
                ClientID = client.ClientID,
                UserID = client.UserID,
                EmergencyContactName = client.EmergencyContactName,
                EmergencyContactPhone = client.EmergencyContactPhone,
                IsReadyToStart = client.IsReadyToStart,
                HasCompletedPayment = client.HasCompletedPayment,
                CreatedAt = client.CreatedAt,
                CreatedBy = client.CreatedBy
            };
        }
    }
}
