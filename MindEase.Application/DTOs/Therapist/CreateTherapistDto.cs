

namespace MindEase.Application.DTOs.Therapist
{
    public class CreateTherapistDto
    {
            
            public int YearsOfExperience { get; set; }
            public decimal HourlyRate { get; set; }
            public string Bio { get; set; }
            public string CertificateURL { get; set; }
            public string ResumeURL { get; set; }
            public int MaxClientsPerWeek { get; set; }
            public bool IsAcceptingNewClients { get; set; }


    }
}
