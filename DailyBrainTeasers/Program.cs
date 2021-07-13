using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyBrainTeasers.Services;
using Microsoft.Extensions.Configuration;

namespace DailyBrainTeasers
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var emailStrings = config.GetSection("Emails").GetChildren();
            var teaserStrings = config.GetSection("Quiz").GetChildren();
            
            // string[] lines = File.ReadAllLines(@"C:\Dev\clients\DailyBrainTeasers\DailyBrainTeasers\brainteasers.txt");
            // Random random = new Random();
            // var randomLineNumber = random.Next(0, lines.Length - 1);
            // var line = lines[randomLineNumber];
            
            // Console.WriteLine($"{line}");
            
            
            // foreach (var line in  lines[randomLineNumber])
            // {
            //     var singleLine = lines[randomLineNumber];
            //     Console.WriteLine($"{singleLine}");
            // }

            var mailList = new List<EmailRecipientDto>();
            foreach (var email in emailStrings)
            {
                mailList.Add(new EmailRecipientDto()
                {
                    DisplayName = email.Key,
                    Email = email.Value
                });

            }
            
            
            var teaserMail = new MailDto()
            {
                Subject = "Brain Teaser",
                Recipients = mailList
            
            };
            
            var mailService = new SmtpMailService();
            var reportMailService = new BrainTeaserMailService(mailService);
            Console.WriteLine("Sending Mail");
            await reportMailService.SendBrainTeaserMail(teaserMail);
            Console.WriteLine("Mail Sent");

        }
    }
}