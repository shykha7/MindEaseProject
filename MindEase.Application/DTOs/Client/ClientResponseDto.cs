
namespace MindEase.Application.DTOs.Client
{
    public class ClientResponseDto
    {
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool IsReadyToStart { get; set; }
        public bool HasCompletedPayment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
