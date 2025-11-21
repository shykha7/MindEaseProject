

using Dapper;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Domain;
using System.Data;

namespace MindEase.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection _db;

        public ClientRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateClientAsync(Client client)
        {
            var sql = @"INSERT INTO Clients 
                        (UserID, EmergencyContactName, EmergencyContactPhone, IsReadyToStart, HasCompletedPayment, CreatedBy) 
                        VALUES 
                        (@UserID, @EmergencyContactName, @EmergencyContactPhone, @IsReadyToStart, @HasCompletedPayment, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _db.ExecuteScalarAsync<int>(sql, client);
        }

        public async Task<Client?> GetClientByIdAsync(int clientId)
        {
            var sql = @"SELECT * FROM Clients WHERE ClientID = @clientId";
            return await _db.QueryFirstOrDefaultAsync<Client>(sql, new { clientId });
        }

        public async Task<Client?> GetClientByUserIdAsync(int userId)
        {
            var sql = @"SELECT * FROM Clients WHERE UserID = @userId";
            return await _db.QueryFirstOrDefaultAsync<Client>(sql, new { userId });
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var sql = @"SELECT * FROM Clients";
            return await _db.QueryAsync<Client>(sql);
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            var sql = @"UPDATE Clients SET
                        EmergencyContactName = @EmergencyContactName,
                        EmergencyContactPhone = @EmergencyContactPhone,
                        IsReadyToStart = @IsReadyToStart,
                        HasCompletedPayment = @HasCompletedPayment
                        WHERE ClientID = @ClientID";

            var rows = await _db.ExecuteAsync(sql, client);
            return rows > 0;
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            var sql = @"DELETE FROM Clients WHERE ClientID = @clientId";
            var rows = await _db.ExecuteAsync(sql, new { clientId });
            return rows > 0;
        }
    }
}
