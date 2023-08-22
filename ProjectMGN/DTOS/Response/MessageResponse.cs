namespace ProjectMGN.DTOS.Response;

public class MessageResponse
{
    public class MessageDto
    {
        public string Content { get; set; }
        public string Type { get; set; }
    }

    public MessageDto Message = new()
    {
        Content = "",
        Type = "",
    };
}