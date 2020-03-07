using BirthdayGreetings.DataAccess;
using System;
using System.Collections.Generic;

namespace BirthdayGreetings.Interfaces
{
    public interface IMessageSenderBusiness
    {
        void SendMessage(Dictionary<string, string> emailAddressAndMessage);

        void SendMessage(List<Tuple<string, string>> emailAddressAndMessage);
    }
}
