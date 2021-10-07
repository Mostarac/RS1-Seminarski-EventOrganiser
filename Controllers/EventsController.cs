using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapp.Services.Interfaces;

namespace webapp.Controllers
{
    [Authorize(Roles = "User")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IUserService _userService;
        private readonly IEventRatingService _eventRatingService;

        public EventsController(IEventService eventService, IEmailSenderService emailSenderService, IUserService userService, IEventRatingService eventRatingService)
        {
            _eventService = eventService;
            _emailSenderService = emailSenderService;
            _userService = userService;
            _eventRatingService = eventRatingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Read(int id)
        {
            var model = _eventService.GetVMById(id);
            model.SimilarEvents = _eventService.GetSimilarEvents(model.EventType, model.Id);

            return View(model);
        }

        public IActionResult SendMessage(int id, string message)
        {
            var eventModel = _eventService.GetById(id);
            var userTo = _userService.GetById(eventModel.CreatedBy);

            var user = _userService.GetById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _emailSenderService.Execute(eventModel.Name, message, userTo.Email, user.Email, user.FirstName + user.LastName);
            ModelState.Clear();

            return new EmptyResult();
        }

        public IActionResult EventRating(int id, double rating)
        {
            if (rating < 1) return new EmptyResult();

            var eventModel = _eventService.GetById(id);
            var user = _userService.GetById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _eventRatingService.AddRating((int)rating, user.Id, id);
            _eventRatingService.UpdateEventRating(id, (int)rating);

            return new EmptyResult();
        }
    }
}