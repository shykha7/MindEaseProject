

namespace MindEase.Application.DTOs.Therapist
{
    public class TherapistResponseDto
    {
        public int TherapistID { get; set; }
        public int UserID { get; set; }
        public required string Bio { get; set; }
        public decimal HourlyRate { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerificationDate { get; set; }
        public int? MaxClientsPerWeek { get; set; }
        public bool IsAcceptingNewClients { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string CertificateURL { get; set; }
        public required string ResumeURL { get; set; }


    }
}
