namespace ProjectMGN.Models;

public class MessagesModel
{
    private string _status = "INFO";
    private readonly List<string> _availableStatuses = new List<string>() { "ERROR", "SUCCESS", "INFO" };

    public string Status
    {
        get => _status;
        set
        {
            if (!_availableStatuses.Contains(value))
            {
                throw new Exception("Invalid status type");
            }
            _status = value;
        }
    }

    public string Message = String.Empty;
}