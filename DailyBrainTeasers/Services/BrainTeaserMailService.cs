using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyBrainTeasers.Services
{
    public class BrainTeaserMailService
    {
        private readonly IMailService _mailservice;


        public BrainTeaserMailService(IMailService mailService)
        {
            _mailservice = mailService;
        }

        public async Task SendBrainTeaserMail(MailDto mail)
        {
            var textBody = @$"Good day,

Here comes your daily brain teaser to solve bitch.

";

            var htmlBody = $@"Good day,<br/> 
<br/>
Here comes your daily brain teaser to solve bitch.<br/>
 
                <br/>";
            var subject = mail.Subject;

            var recipients = new List<EmailRecipientDto> { };

            foreach (var email in mail.Recipients)
            {
                recipients.Add(new EmailRecipientDto()
                    {
                        DisplayName = email.DisplayName,
                        Email = email.Email
                    }
                );
            }

            await _mailservice.SendMail(recipients, subject, textBody, htmlBody,false);
        }
    }
}

