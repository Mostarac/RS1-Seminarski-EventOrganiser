using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;

namespace webapp.Areas.Organizer.Helpers
{
    public class SmsHelper
    {

        public static void SendSms(string to, string from, string smsText)
        {
            try
            {
                var credentials = Credentials.FromApiKeyAndSecret("keys", "here");
                var client = new SmsClient(credentials);
                var request = new SendSmsRequest { To = to, From = from, Text = smsText };
                var response = client.SendAnSms(request);
            }
            catch (VonageSmsResponseException ex)
            {
                var emailBodySMSexception = "We couldn't send SMS to " + to + ", Vonage exception response was: " + ex.Message;
                Email.SendEmail("someEmail@gmail.com", "Emir", "someEmail@gmail.com", "Emir", "SMS failure", emailBodySMSexception, "smtp.gmail.com", "someEmail@gmail.com", "PasswordHere");
            }
        }
        
    }
}
