namespace ProjectMGN.Models;

public class MessagesModel
{
    private string _type = "INFO";
    private readonly List<string> _availableStatuses = new (){ "ERROR", "SUCCESS", "INFO" };

    public string Status
    {
        get => _type;
        set
        {
            if (!_availableStatuses.Contains(value))
            {
                throw new Exception("Invalid status type");
            }
            _type = value;
        }
    }

    public string Content { get; set; } = "";
}