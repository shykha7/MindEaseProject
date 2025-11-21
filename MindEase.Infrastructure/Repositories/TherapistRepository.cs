
using Dapper;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Domain;
using System.Data;

namespace MindEase.Infrastructure.Repositories
{
    public class TherapistRepository : ITherapistRepository
    {
        private readonly IDbConnection _db;
        public TherapistRepository(IDbConnection db)
        {
            _db = db;
        }

       public async Task<int> CreateTherapistAsync(Therapist therapist)
        {
            var sql = @"
        INSERT INTO Therapists 
        (UserID, YearsOfExperience, HourlyRate, Bio, CertificateURL, ResumeURL,
         IsVerified, VerificationDate, MaxClientsPerWeek, IsAcceptingNewClients, CreatedBy) 
        VALUES 
        (@UserID, @YearsOfExperience, @HourlyRate, @Bio, @CertificateURL, @ResumeURL,
         @IsVerified, @VerificationDate, @MaxClientsPerWeek, @IsAcceptingNewClients, @CreatedBy);

        SELECT CAST(SCOPE_IDENTITY() AS INT);
             ";

            
            var newId = await _db.ExecuteScalarAsync<int>(sql, therapist);
            return newId;
        }

        public async Task<Therapist?> GetTherapistByIdAsync(int therapistId)
        {
            var sql = @"SELECT * FROM Therapists WHERE TherapistID = @therapistId";

            return await _db.QueryFirstOrDefaultAsync<Therapist>(sql, new { therapistId });
        }

        public async Task<Therapist?> GetTherapistByUserIdAsync(int userId)
        {
            var sql = @"SELECT * FROM Therapists WHERE UserID = @userId";

            return await _db.QueryFirstOrDefaultAsync<Therapist>(sql, new { userId });
        }

        public async Task<IEnumerable<Therapist>> GetAllTherapistsAsync()
        {
            var sql = @"SELECT * FROM Therapists";

            return await _db.QueryAsync<Therapist>(sql);
        }

        public async Task<bool> UpdateTherapistAsync(Therapist therapist)
        {
            var sql = @"
                UPDATE Therapists
                SET 
                    YearsOfExperience = @YearsOfExperience,
                    HourlyRate = @HourlyRate,
                    Bio = @Bio,
                    CertificateURL = @CertificateURL,
                    ResumeURL = @ResumeURL,
                    IsVerified = @IsVerified,
                    VerificationDate = @VerificationDate,
                    MaxClientsPerWeek = @MaxClientsPerWeek,
                    IsAcceptingNewClients = @IsAcceptingNewClients
                WHERE TherapistID = @TherapistID;
            ";

            var rows = await _db.ExecuteAsync(sql, therapist);
            return rows > 0;
        }

        public async Task<bool> DeleteTherapistAsync(int therapistId)
        {
            var sql = @"DELETE FROM Therapists WHERE TherapistID = @therapistId";

            var rows = await _db.ExecuteAsync(sql, new { therapistId });
            return rows > 0;
        }

    }
}
