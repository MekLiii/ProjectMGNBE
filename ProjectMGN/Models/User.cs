using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace ProjectMGN.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; } 

        public string? UserName { get; set; } 


    }
}
