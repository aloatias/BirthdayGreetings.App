using BirthdayGreetings.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BirthdayGreeting.Business
{
    public class TextSenderBusiness : IMessageSenderBusiness
    {
        private readonly ILogger<TextSenderBusiness> _logger;

        public TextSenderBusiness(ILogger<TextSenderBusiness> logger)
        {
            _logger = logger;
        }

        public void SendMessage(Dictionary<string, string> emailAddressAndMessage)
        {
            
        }
    }
}
