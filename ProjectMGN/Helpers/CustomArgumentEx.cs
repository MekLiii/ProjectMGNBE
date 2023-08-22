using ProjectMGN.Models;

namespace ProjectMGN.Helpers;

public class CustomArgumentEx : ArgumentException
{
        private MessagesModel Messages { get; }

        public CustomArgumentEx(MessagesModel messages) : base(messages.Message)
        {
                Messages = messages;
        }
}