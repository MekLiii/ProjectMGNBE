using ProjectMGN.Models;

namespace ProjectMGN.Messages;

public class CustomMessages
{
    public readonly MessagesModel InvalidUserName = new()
    {
        Content = "Invalid password",
        Status = "ERROR"
    };

    public readonly MessagesModel UserCreatedSuccessFully = new()
    {
        Content = "User created successfully",
        Status = "SUCCESS"
    };

}