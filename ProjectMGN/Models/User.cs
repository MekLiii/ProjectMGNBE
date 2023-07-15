using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace ProjectMGN.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;


    }
}
