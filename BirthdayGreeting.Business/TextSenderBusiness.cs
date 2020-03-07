using BirthdayGreetings.Interfaces;
using Microsoft.Extensions.Logging;
using System;
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

        public void SendMessage(Dictionary<string, string> phoneNumberAndMessage)
        {
            foreach (var contact in phoneNumberAndMessage)
            {
                _logger.LogInformation($"Sending text message: { contact.Value } to { contact.Key }");
                Console.WriteLine($"Sending message: { contact.Value } to { contact.Key }");
                Console.WriteLine();
            }
        }

        public void SendMessage(List<Tuple<string, string>> emailAddressAndMessage)
        {
            emailAddressAndMessage.ForEach(c =>
            {
                _logger.LogInformation($"Sending text message: { c.Item2 } to { c.Item1 }");
                Console.WriteLine($"Sending text message: { c.Item2 } to { c.Item1 }");
                Console.WriteLine();
            });
        }
    }
}
