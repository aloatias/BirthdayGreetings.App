using BirthdayGreetings.Interfaces;
using System;
using System.Collections.Generic;

namespace BirthdayGreeting.Business
{
    public class MailSenderBusiness : IMessageSenderBusiness
    {
        public void SendMessage(Dictionary<string, string> emailAddressAndMessage)
        {
            throw new NotImplementedException();
        }
    }
}
