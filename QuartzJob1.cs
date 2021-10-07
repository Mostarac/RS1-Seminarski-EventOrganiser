using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vonage.Messaging;
using Vonage.Request;
using webapp.Areas.Organizer.Helpers;
using webapp.Context;
using webapp.Models;

namespace webapp
{
    public class QuartzJob1 : IJob
    {
        private readonly ApplicationDbContext _db;
        public QuartzJob1(ApplicationDbContext context)
        {
            _db = context;
        }

        public virtual Task Execute(IJobExecutionContext context)
        {
            var totalCommentsToday = _db.Comment.Where(x => x.CreatedDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).Count();

            Event mostCommentedEvent = null;
            int mostComments = 0;

            var events = _db.Event.ToList();

            foreach(var e in events)
            {
                var howManyToday = _db.Comment.Where(x => x.EventId == e.Id && x.CreatedDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).Count();
                if(howManyToday > mostComments)
                {
                    mostCommentedEvent = e;
                    mostComments = howManyToday;
                }
            }

            string messageToSend = "";

            if(mostCommentedEvent == null)
            {
                messageToSend = "Event Organizer daily comment overview: no comments on any events were made today.";
            }
            else
            {
                messageToSend = "Total comments today: " + totalCommentsToday.ToString() + ". Most commented event is " + mostCommentedEvent.Name + " with " + mostComments.ToString() + " comments.";
            }

            //var jobKey = context.JobDetail.Key;
            //Console.WriteLine($"SimpleJob says: {jobKey} executing at {DateTime.Now:r}");
            var emailBody = "Daily comment overview sent at: " + DateTime.Now.ToString("dd/MM/yyyy") + ". ";
            emailBody = messageToSend;
            Email.SendEmail("someEmail@gmail.com", "Emir", "someEmail@gmail.com", "Emir", "Quartz service testing", emailBody, "smtp.gmail.com", "someEmail@gmail.com", "PasswordHere");

            string to = "387620555666";
            string from = "Vonage APIs";
            string smsText = messageToSend;

            SmsHelper.SendSms(to, from, smsText);

            return Task.CompletedTask;
        }
    }
}
