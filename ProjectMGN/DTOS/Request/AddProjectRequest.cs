namespace ProjectMGN.DTOS.Request
{
    public class AddProjectRequest
    {
        public string? ProjectName { get; set; }
        public string? Image { get; set; }
        public int? ConfigurationId { get; set; }
    }
}