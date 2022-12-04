using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SendEmail : SmtpClient
    {
        public SendEmail() : base("marcosousa.eu")
        {
            Credentials = new NetworkCredential("rasbet@marcosousa.eu", "3u01Lt%0c");
        }

        public void Send(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Subject = subject,
                Body = body,
                From = new("rasbet@marcosousa.eu")
            };
            mailMessage.To.Add(to);
            SendAsync(mailMessage, null);
        }
    }
}
