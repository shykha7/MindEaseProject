

namespace MindEase.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public int? UserID { get; set; }
        public string? FullName { get; set; }
        public string? UserType { get; set; }
    }
}
