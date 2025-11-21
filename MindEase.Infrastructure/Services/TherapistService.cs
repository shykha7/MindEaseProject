

using Microsoft.Extensions.Configuration;
using MindEase.Application.DTOs.Therapist;
using MindEase.Application.Interfaces.Repositories;
using MindEase.Application.Interfaces.Services;
using MindEase.Domain;

namespace MindEase.Infrastructure.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly IConfiguration _config;

        public TherapistService(ITherapistRepository therapistRepository, IConfiguration configuration)
        {
            _therapistRepository = therapistRepository;
            _config = configuration;
        }

        public async Task<int> CreateTherapistAsync(CreateTherapistDto dto, int createdByUserId)
        {
            var therapist = new Therapist
            {
                UserID = createdByUserId,
                YearsOfExperience = dto.YearsOfExperience,
                HourlyRate = dto.HourlyRate,
                Bio = dto.Bio,
                CertificateURL = dto.CertificateURL,
                ResumeURL = dto.ResumeURL,
                MaxClientsPerWeek = dto.MaxClientsPerWeek,
                IsAcceptingNewClients = dto.IsAcceptingNewClients,
                CreatedBy = createdByUserId
            };

            return await _therapistRepository.CreateTherapistAsync(therapist);
        }

        public async Task<TherapistResponseDto?> GetTherapistByIdAsync(int therapistId)
        {
            var therapist = await _therapistRepository.GetTherapistByIdAsync(therapistId);
            if (therapist == null)
                return null;

            return MapToResponseDto(therapist);
        }

        public async Task<TherapistResponseDto?> GetTherapistByUserIdAsync(int userId)
        {
            var therapist = await _therapistRepository.GetTherapistByUserIdAsync(userId);
            if (therapist == null)
                return null;

            return MapToResponseDto(therapist);
        }

        public async Task<IEnumerable<TherapistResponseDto>> GetAllTherapistsAsync()
        {
            var list = await _therapistRepository.GetAllTherapistsAsync();
            return list.Select(t => MapToResponseDto(t));
        }

        public async Task<bool> UpdateTherapistAsync(int therapistId, UpdateTherapistDto dto)
        {
            var existing = await _therapistRepository.GetTherapistByIdAsync(therapistId);
            if (existing == null)
                return false;

            existing.YearsOfExperience = dto.YearsOfExperience;
            existing.HourlyRate = dto.HourlyRate;
            existing.Bio = dto.Bio;
            existing.CertificateURL = dto.CertificateURL;
            existing.ResumeURL = dto.ResumeURL;
            existing.MaxClientsPerWeek = dto.MaxClientsPerWeek;
            existing.IsAcceptingNewClients = dto.IsAcceptingNewClients ?? existing.IsAcceptingNewClients;

            return await _therapistRepository.UpdateTherapistAsync(existing);
        }

        public async Task<bool> DeleteTherapistAsync(int therapistId)
        {
            return await _therapistRepository.DeleteTherapistAsync(therapistId);
        }

        private TherapistResponseDto MapToResponseDto(Therapist t)
        {
            return new TherapistResponseDto
            {
                TherapistID = t.TherapistID,
                UserID = t.UserID,
                YearsOfExperience = t.YearsOfExperience ?? 0,
                HourlyRate = t.HourlyRate??0,
                Bio = t.Bio,
                CertificateURL = t.CertificateURL,
                ResumeURL = t.ResumeURL,
                IsVerified = t.IsVerified,
                VerificationDate = t.VerificationDate,
                MaxClientsPerWeek = t.MaxClientsPerWeek,
                IsAcceptingNewClients = t.IsAcceptingNewClients,
                CreatedAt = t.CreatedAt
            };
        }
    }
}
