using System.Buffers.Text;

namespace ProjectMGN.Models
{
    public class Project
    {
        public int Id { get; set; } = 0;
        public string Guid { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public int OwnerId { get; set; } = 0;
        public int? ConfigurationId { get; set; } = 0;

        public string? Image { get; set; }
    }
}
