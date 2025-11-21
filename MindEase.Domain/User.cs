

namespace MindEase.Domain
{
    public class User
    {
        public int UserID { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public int FailedLoginAttempts { get; set; } = 0;
        public bool IsLocked { get; set; } = false;
        public DateTime? PasswordChangedDate { get; set; }
        public required string UserType { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
        public int? CreatedBy { get; set; }


    }
}
