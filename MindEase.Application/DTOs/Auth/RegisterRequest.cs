

namespace MindEase.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string UserType { get; set; }  // Client or Therapist
        public int? CreatedBy { get; set; }
    }
}
