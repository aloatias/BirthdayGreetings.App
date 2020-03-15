using BirthdayGreetings.DataAccess;
using BirthdayGreetings.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BirthdayGreetings.Business
{
    public class MessageParserBusiness : IMessageParserBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="peopleOnBirthday"></param>
        /// <param name="peopleToNotify"></param>
        /// <returns></returns>
        public List<Tuple<Person, string>> CreateIndividualBirthdayReminder(List<Person> peopleOnBirthday, List<Person> peopleToNotify)
        {
            var contactAndMessage = new List<Tuple<Person, string>>();

            peopleToNotify.ForEach(pTn =>
            {
                peopleOnBirthday.ForEach(pOb =>
                {
                    if (pTn != pOb)
                    {
                        var sb = new StringBuilder();

                        sb.AppendLine("Subject: Birthday Reminder!");
                        sb.AppendLine();
                        sb.AppendLine($"Dear {pTn.FirstName }");
                        sb.AppendLine();
                        sb.AppendLine($"Today is { pOb.FirstName } { pOb.LastName }' birthday.");
                        sb.AppendLine("Don't forget to send them a message!");

                        contactAndMessage.Add(Tuple.Create(pTn, sb.ToString()));
                    }
                });
            });

            return contactAndMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peopleOnBirthday"></param>
        /// <returns></returns>
        public Dictionary<Person, string> CreatePersonalBirthdayWish(List<Person> peopleOnBirthday)
        {
            var filePath = Path.GetFullPath("PersonalBirthdayWish.txt");

            string template;
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                template = streamReader.ReadToEnd();
            };

            var contactAndMessage = new Dictionary<Person, string>();

            peopleOnBirthday.ForEach(p =>
            {
                var message = template.Replace("{ name }", p.FirstName);

                contactAndMessage.Add(p, message);
            });

            return contactAndMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peopleOnBirthday"></param>
        /// <param name="peopleToNotify"></param>
        /// <returns></returns>
        public Dictionary<Person, string> CreateGeneralBirthdayReminder(List<Person> peopleOnBirthday, List<Person> peopleToNotify)
        {
            var contactAndMessage = new Dictionary<Person, string>();

            peopleToNotify.ForEach(rp =>
            {
                var message = new StringBuilder();

                message.AppendLine("Subject: Birthday Reminder!");
                message.AppendLine($"Dear: { rp.FirstName },");
                message.AppendLine();
                message.Append("Today is ");

                bool atLeastOneMessage = false;
                for (var i = 0; i < peopleOnBirthday.Count; i++)
                {
                    if (peopleOnBirthday[i] != peopleToNotify[i])
                    {
                        atLeastOneMessage = true;

                        message.Append($"{ peopleOnBirthday[i].FirstName } { peopleOnBirthday[i].LastName }");

                        if (i + 2 != peopleOnBirthday.Count)
                        {
                            message.Append(", ");
                        }
                        else
                        {
                            message.Append($" and { peopleOnBirthday[i++].FirstName } { peopleOnBirthday[i++].LastName }");
                            i += 2;
                        }
                    }
                }

                if (atLeastOneMessage)
                {
                    message.AppendLine("'s birthday.");
                    message.AppendLine("Don't forget to send them a message!");
                    contactAndMessage.Add(rp, message.ToString());
                }
            });

            return contactAndMessage;
        }
    }
}
