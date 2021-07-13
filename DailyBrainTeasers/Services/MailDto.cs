using System.Collections.Generic;

namespace DailyBrainTeasers.Services
{
    public class MailDto
    {
        public string Subject { get; set; }
        public List<EmailRecipientDto> Recipients { get; set; }

    }
}