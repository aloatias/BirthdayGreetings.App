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

        public void SendMessage(Dictionary<string, string> phoneNumberAndMessage)
        {
            foreach (var contact in phoneNumberAndMessage)
            {
                SendMessage(contact.Value, contact.Key);
            }
        }

        public void SendMessage(List<Tuple<string, string>> emailAddressAndMessage)
        {
            emailAddressAndMessage.ForEach(c =>
            {
                SendMessage(c.Item2, c.Item1);
            });
        }

        #region Private Method
        private void SendMessage(string message, string contact)
        {
            _logger.LogInformation($"Sending email message: { message } to { contact }");
            Console.WriteLine($"Sending email message: { message } to { contact }");
            Console.WriteLine();
        }
        #endregion
    }
}
