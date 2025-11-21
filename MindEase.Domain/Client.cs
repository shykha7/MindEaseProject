

namespace MindEase.Domain
{
    public class Client
    {

        public int ClientID { get; set; }
        public int UserID { get; set; }            // FK to Users
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public bool IsReadyToStart { get; set; } = false;
        public bool HasCompletedPayment { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? CreatedBy { get; set; }
    }
}
