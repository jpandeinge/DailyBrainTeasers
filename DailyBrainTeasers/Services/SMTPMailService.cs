using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace DailyBrainTeasers.Services
{
    public class SmtpMailService : IMailService
    {
        private readonly SmtpClient _smtpClient;
        private MailboxAddress fromMail;
        private string host;
        private int port;
        private string username;
        private string password;
        private bool useSsl;

        public SmtpMailService()
        {
            fromMail = new MailboxAddress("Daily Brain Teasers", "myusername@gmail.com");
            host = "smtp.gmail.com";
            username = "myusername@gmail.com";
            password = "mypwd";
            port = 587;
            useSsl = false;
        }


        public async Task SendMail(List<EmailRecipientDto> recipients, string subject, string textBody, string htmlBody, bool important = false)
        {
            Console.WriteLine($"Attempting to send email to {recipients[0].Email}");

            var message = new MimeMessage();
            message.From.Add(fromMail);
            foreach (var recipient in recipients)
            {
                message.To.Add(new MailboxAddress(recipient.DisplayName, recipient.Email));
            }

            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.TextBody = textBody;
            builder.HtmlBody = htmlBody;
            message.Body = builder.ToMessageBody();
            if (important)
                message.Importance = MessageImportance.High;

            using var client = new SmtpClient();
            client.Connect(host, port, SecureSocketOptions.StartTls);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(username, password);
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public interface IMailService
    {
        Task SendMail(List<EmailRecipientDto> recipients, string subject, string textBody, string htmlBody, bool important = false);
        
    }
}