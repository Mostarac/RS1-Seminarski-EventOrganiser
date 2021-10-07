using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace webapp.Areas.Organizer.Helpers
{
    public class Email
    {
        public static void SendEmail(string fromMail, string fromName, string toMail, string toName, string subject, string body, string smtpAddress, string credentialMail, string credentialPassword)
        {
            using (MailMessage emailMessage = new MailMessage())
            {
                //emailMessage.From = new MailAddress("account2@gmail.com", "Account2");
                emailMessage.From = new MailAddress(fromMail, fromName);
                //emailMessage.To.Add(new MailAddress("account1@gmail.com", "Account1"));
                emailMessage.To.Add(new MailAddress(toMail, toName));
                emailMessage.Subject = subject;
                emailMessage.Body = body;
                emailMessage.Priority = MailPriority.Normal;
                //using (SmtpClient MailClient = new SmtpClient("smtp.gmail.com", 587))//or 465 if 587 is not working
                using (SmtpClient MailClient = new SmtpClient(smtpAddress, 587))//or 465 if 587 is not working
                {
                    MailClient.EnableSsl = true;
                    //MailClient.Credentials = new System.Net.NetworkCredential("account2@gmail.com", "password");
                    MailClient.Credentials = new System.Net.NetworkCredential(credentialMail, credentialPassword);
                    MailClient.Send(emailMessage);
                }
                //https://stackoverflow.com/questions/5336239/attach-a-file-from-memorystream-to-a-mailmessage-in-c-sharp
            }
        }
    }
}

