namespace ProjectMGN.Models
{
    public class Actions
    {
        public int? Id { get; set; }
        public string? ActionName { get; set; }
        public int ConfigurationId { get; set; }
        public int? commandId { get; set; }
    }
}
