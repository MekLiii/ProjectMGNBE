namespace ProjectMGN.DTOS.Request;

public class CreateActionRequest
{
    // public int Id { get; set; }
    public string? ActionName { get; set; }
    public int? ConfigurationId { get; set; }
    public int CommandId { get; set; }
    public int OwnerId { get; set; }
}