

namespace MindEase.Application.DTOs.Client
{
    public class UpdateClientDto
    {
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool? IsReadyToStart { get; set; }
        public bool? HasCompletedPayment { get; set; }
    }
}
