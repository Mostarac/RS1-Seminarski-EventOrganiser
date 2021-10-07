using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using webapp.Services.Interfaces;

namespace webapp.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        const string apiKey = "SG.wnQIC1OiS6CiiiTfN4cTFw.UGFDLCM0fmtXcSa5TAO9V4dF7BWiQISCuGiyI9wTtgc";

        public EmailSenderService()
        {
        }

        public Task Execute(string subject, string message, string emailTo, string emailFrom, string name)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(emailFrom, name),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(emailTo));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
