using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using webapp.Areas.Organizer.Repositories;
using webapp.Context;
using webapp.Models;

namespace webapp.Areas.Organizer.Helpers
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IExceptionRepository _exceptionRepository;
        private readonly ApplicationDbContext _context;
        public CustomExceptionFilter(IExceptionRepository exceptionRepository, ApplicationDbContext context)
        {
            _exceptionRepository = exceptionRepository;
            _context = context;
        }

        public override void OnException(ExceptionContext exception)
        {
            var log = new ExceptionLog
            {
                TimeStamp = DateTime.UtcNow,
                ActionDescriptor = exception.ActionDescriptor.DisplayName,
                IpAddress = exception.HttpContext.Connection.RemoteIpAddress.ToString(),
                Message = exception.Exception.Message,
                RequestId = Activity.Current?.Id ?? exception.HttpContext.TraceIdentifier,
                RequestPath = exception.HttpContext.Request.Path,
                Source = exception.Exception.Source,
                StackTrace = exception.Exception.StackTrace,
                Type = exception.Exception.GetType().ToString(),
                User = exception.HttpContext.User.Identity.Name
            };
            _exceptionRepository.Add(log);

            string emailBody = "Exception occured in EventOrganizer app, timestamp: " + log.TimeStamp.ToString("dd/MM/yyyy") + ", user was: ," + log.User + ", message was: " + log.Message;

            Email.SendEmail("someEmail@gmail.com", "Emir", "someEmail@gmail.com", "Emir", "Exception in EventOrganizer", emailBody, "smtp.gmail.com", "someEmail@gmail.com", "PasswordHere");

        }
    }
}
