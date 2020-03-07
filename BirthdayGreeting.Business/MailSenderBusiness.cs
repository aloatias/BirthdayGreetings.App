using BirthdayGreetings.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BirthdayGreeting.Business
{
    public class MailSenderBusiness : IMessageSenderBusiness
    {
        private readonly ILogger<MailSenderBusiness> _logger;

        public MailSenderBusiness(ILogger<MailSenderBusiness> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddressAndMessage"></param>
        public void SendMessage(Dictionary<string, string> emailAddressAndMessage)
        {
            foreach (var contact in emailAddressAndMessage)
            {
                _logger.LogInformation($"Sending email: { contact.Value } to { contact.Key }");
                Console.WriteLine($"Sending email: { contact.Value } to { contact.Key }");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddressAndMessage"></param>
        public void SendMessage(List<Tuple<string, string>> emailAddressAndMessage)
        {
            emailAddressAndMessage.ForEach(c => {
                _logger.LogInformation($"Sending email: { c.Item2 } to { c.Item1 }");
                Console.WriteLine($"Sending email: { c.Item2 } to { c.Item1 }");
                Console.WriteLine();
            });
        }
    }
}
