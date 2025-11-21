

namespace MindEase.Domain
{
    public class Therapist
    {
        public int TherapistID { get; set; }
        public int UserID { get; set; }

        public int? YearsOfExperience { get; set; }
        public decimal? HourlyRate { get; set; }
        public string? Bio { get; set; }
        public string? CertificateURL { get; set; }
        public string? ResumeURL { get; set; }

        public bool IsVerified { get; set; }
        public DateTime? VerificationDate { get; set; }

        public int? MaxClientsPerWeek { get; set; }
        public bool IsAcceptingNewClients { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
