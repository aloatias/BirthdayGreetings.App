using BirthdayGreetings.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BirthdayGreetings.Business
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
                SendMessage(contact.Value, contact.Key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddressAndMessage"></param>
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
