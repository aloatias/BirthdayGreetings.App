using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayGreetings.Interfaces
{
    public interface IMessageSender
    {
        void SendMessage(string message, string mailTo);
    }
}
