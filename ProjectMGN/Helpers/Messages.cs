using ProjectMGN.Models;

namespace ProjectMGN.Helpers;

public class Messages
{
    public MessagesModel InvalidUserName = new()
    {
        Message = "Invalid password",
        Status = "ERROR"
    };
}