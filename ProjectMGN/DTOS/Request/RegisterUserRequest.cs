using System.ComponentModel.DataAnnotations;

namespace ProjectMGN.DTOS.Request
{
    public class RegisterUserRequest
    {
        public string? UserName { get; set; } = null!;
        public string? Password { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; } = null!;
    }
}
