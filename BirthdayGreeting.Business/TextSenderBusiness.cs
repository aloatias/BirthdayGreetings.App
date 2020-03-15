using BirthdayGreetings.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BirthdayGreetings.Business
{
    public class TextSenderBusiness : IMessageSenderBusiness
    {
        private readonly ILogger<TextSenderBusiness> _logger;

        public TextSenderBusiness(ILogger<TextSenderBusiness> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumberAndMessage"></param>
        public void SendMessage(Dictionary<string, string> phoneNumberAndMessage)
        {
            foreach (var contact in phoneNumberAndMessage)
            {
                SendMessage(contact.Value, contact.Key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumberAndMessage"></param>
        public void SendMessage(List<Tuple<string, string>> phoneNumberAndMessage)
        {
            phoneNumberAndMessage.ForEach(c =>
            {
                SendMessage(c.Item2, c.Item1);
            });
        }

        #region Private Methods
        private void SendMessage(string message, string contact) {
            _logger.LogInformation($"Sending text message: { message } to { contact }");
            Console.WriteLine($"Sending text message: { message } to { contact }");
            Console.WriteLine();
        }
        #endregion
    }
}
