namespace ProjectMGN.Models
{
    public class Project
    {
        public int Id { get; set; } = 0;
        public string Guid { get; set; }
        public string? Name { get; set; }
        public int? OwnerId { get; set; }
        public int? ConfigurationId { get; set; }

        public string? Image { get; set; }
    }
}