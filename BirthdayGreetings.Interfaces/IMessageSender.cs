using System.Collections.Generic;

namespace BirthdayGreetings.Interfaces
{
    public interface IMessageSenderBusiness
    {
        void SendMessage(Dictionary<string, string> emailAddressAndMessage);
    }
}
