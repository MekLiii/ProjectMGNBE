using System.Buffers.Text;

namespace ProjectMGN.Models
{
    public class Projects
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public int OwnerId { get; set; } = 0;
        public int? ConfigurationId { get; set; } = 0;

        public string? Image { get; set; } = String.Empty;
    }
}
