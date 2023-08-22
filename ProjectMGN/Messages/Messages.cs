using ProjectMGN.Models;

namespace ProjectMGN.Messages;

public class Messages
{
    public MessagesModel InvalidUserName = new()
    {
        Message = "Invalid password",
        Status = "ERROR"
    };
}