

namespace MindEase.Application.DTOs.Client
{
    public class CreateClientDto
    {
        public int UserID { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool IsReadyToStart { get; set; } = false;
        public bool HasCompletedPayment { get; set; } = false;
    }
}
