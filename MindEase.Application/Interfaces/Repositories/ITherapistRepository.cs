

using MindEase.Domain;

namespace MindEase.Application.Interfaces.Repositories
{
    public interface ITherapistRepository
    {
        Task<int> CreateTherapistAsync(Therapist therapist);
        Task<Therapist?> GetTherapistByIdAsync(int therapistId);
        Task<Therapist?> GetTherapistByUserIdAsync(int userId);
        Task<IEnumerable<Therapist>> GetAllTherapistsAsync();
        Task<bool> UpdateTherapistAsync(Therapist therapist);
        Task<bool> DeleteTherapistAsync(int therapistId);
    }
}
