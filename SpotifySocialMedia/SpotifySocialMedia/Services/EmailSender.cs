using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace SpotifySocialMedia.Services
{
    public class EmailSender: IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public AuthMessageSenderOptions Options { get; set; }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           
            await Execute(Options.GmailPassword, subject, htmlMessage, email);
        }
        public async Task Execute(string password, string subject, string messageToSend, string toEmail)
        {

            using (SmtpClient smtp = new SmtpClient())
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("dcmieczyslaw@gmail.com");
                    message.To.Add(new MailAddress(toEmail));
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = messageToSend;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("dcmieczyslaw@gmail.com", password);
                    smtp.EnableSsl = true;

                    smtp.Send(message);

                }

            }

        }

    }
}
