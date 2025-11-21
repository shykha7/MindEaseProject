

using MindEase.Application.DTOs.Therapist;

namespace MindEase.Application.Interfaces.Services
{
    public interface ITherapistService
    {
        Task<int> CreateTherapistAsync(CreateTherapistDto dto, int createdByUserId);
        Task<TherapistResponseDto?> GetTherapistByIdAsync(int therapistId);
        Task<TherapistResponseDto?> GetTherapistByUserIdAsync(int userId);
        Task<IEnumerable<TherapistResponseDto>> GetAllTherapistsAsync();
        Task<bool> UpdateTherapistAsync(int therapistId, UpdateTherapistDto dto);
        Task<bool> DeleteTherapistAsync(int therapistId);
    }
}
