using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProjectMGN.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; } = 0;
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; } 

        public string? UserName { get; set; } 


    }
}
