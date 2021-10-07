using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapp.Context;
using webapp.Models;
using Microsoft.EntityFrameworkCore;
using webapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using webapp.Areas.Organizer.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace webapp.Controllers
{
    [Area("Organizer")]
    [Authorize]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NotificationsController _notificationsController;
        private readonly IHostingEnvironment _webHostEnvironment;

        public EventController(ApplicationDbContext context, NotificationsController notificationsController, IHostingEnvironment webHostEnvironment)
        {
            _context = context;
            _notificationsController = notificationsController;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            var events = _context.Event.Include(x => x.EventType).Include(x => x.Venue).Where(x=> x.Venue.AppUserId == userID).Select(x => new EventIndexVmEM
            {

                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Venue = x.Venue.Name,
                VenueType = x.Venue.VenueType.Name,
                StreetAddress = x.Venue.Address.Street,
                City = x.Venue.Address.City.Name,
                Country = x.Venue.Address.City.Country.Name,
                EventType = x.EventType.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Description = x.Description

            }).ToList();

            ViewData["title"] = "My Events";

            ViewData["_LayoutNavbarBG"] = "background-color: rgb(114, 159, 207); color: white; z-index: 1000;";
            ViewData["_layoutBackgroundImg-key"] = "../images/EventIndexBackground.png";
            ViewData["_layoutBackgroundStyle-key"] = "background-size: 30% 60%; background-repeat: repeat; background-attachment: fixed";
            ViewData["eventsIndexList-key"] = events;

            return View();
        }

        public IActionResult Add()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            List<Venue> venues = _context.Venue.Include(vt => vt.VenueType).Where(x=> x.AppUserId == userID).ToList();
            List<EventType> eventTypes = _context.EventType.ToList();


            ViewData["venues-key"] = venues;
            ViewData["eventTypes-key"] = eventTypes;

            EventAddVmEM vm = new EventAddVmEM();

            return View(vm);
        }

        public IActionResult AddEvent(EventAddVmEM model)
        {

            var uniqueFileName = Image.Upload(model.Picture, _webHostEnvironment, "events");

            var newEvent = new Event
            {
                Name = model.Name,
                ImageUrl = uniqueFileName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Description = model.Description,
                VenueId = model.VenueID,
                EventTypeId = model.EventTypeID,
                CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _context.Add(newEvent);

            _context.SaveChanges();

            int EventId = newEvent.Id;

            _notificationsController.CreateEventNotification(model.Name, EventId);

            return RedirectToAction("Index", "RentaGear", new { eventId = EventId });
        }

        public IActionResult Delete(int id)
        {

            Event e = _context.Event.Include(x => x.Venue).Where(x => x.Id == id).SingleOrDefault();

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            if (e == null)
            {
                return Content("Event doesn't exist");
            }

            if (e.Venue.AppUserId != userID)
                return Content("You can delete only your own Events!");

            _context.Remove(e);

            _context.SaveChanges();

            _context.Dispose();

            return RedirectToAction("Index", "Event");
        }

        public IActionResult Edit(int? Id)
        {

            if (Id == null)
            {
                return RedirectToAction("Index", "Event");
            }

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            List<Venue> venues = _context.Venue.Include(vt => vt.VenueType).Where(x=> x.AppUserId == userID).ToList();
            List<EventType> eventTypes = _context.EventType.ToList();

            Event eventEdit = _context.Event.Include(x => x.EventType).Include(x => x.Venue).Where(k => k.Id == Id).FirstOrDefault();

            if (eventEdit.Venue.AppUserId != userID)
                return Content("You can edit only your own Events!");

            ViewData["textarea-key"] = eventEdit.Description;
            ViewData["venues-key"] = venues;
            ViewData["eventTypes-key"] = eventTypes;
            ViewData["eventEdit-key"] = eventEdit;

            EventAddVmEM vm = new EventAddVmEM();

            return View(vm);
        }

        public IActionResult EditEvent(EventAddVmEM eventvm)
        {

            string imageurl = "";

            if (eventvm.Picture == null)
            {
                imageurl = eventvm.ImageUrl;
            }
            else
            {
                imageurl = Image.Upload(eventvm.Picture, _webHostEnvironment, "events");
            }

            Event novi = new Event
            {
                Id = eventvm.Id,
                Name = eventvm.Name,
                ImageUrl = imageurl,
                StartDate = eventvm.StartDate,
                EndDate = eventvm.EndDate,
                Description = eventvm.Description,
                VenueId = eventvm.VenueID,
                EventTypeId = eventvm.EventTypeID
            };

            _context.Event.Update(novi);

            _context.SaveChanges();

            _context.Dispose();

            return RedirectToAction("Index", "RentaGear", new { eventId = eventvm.Id });

        }

        public IActionResult SeedCommentsRatings()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            var events = _context.Event.Include(x=>x.Venue).Where(x => x.Venue.AppUserId == userID).ToList();

            var visitors = _context.UserRoles.Where(x => x.RoleId == _context.Roles.Where(a => a.Name == "User").Select(a => a.Id).SingleOrDefault()).ToList();

            List<AppUser> users = new List<AppUser>();

            foreach(var v in visitors)
            {
                users.Add(_context.Users.Where(x => x.Id == v.UserId).SingleOrDefault());
            }

            Random rnd = new Random();

            foreach (var e in events)
            {
                foreach(var v in visitors)
                {
                    var visitorData = _context.Users.Where(x => x.Id == v.UserId).SingleOrDefault();

                    var newComment = new Comment
                    {
                        EventId = e.Id,
                        UserId = visitorData.Id,
                        Text = "Test comment for event " + e.Name + " made by " + visitorData.UserName,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "Seed"
                    };

                    _context.Add(newComment);
                    _context.SaveChanges();

                    var newRating = new EventRating
                    {
                        EventId = e.Id,
                        UserId = visitorData.Id,
                        Rate = rnd.Next(1, 5),
                        CreatedBy = "Seed",
                        CreatedDate = DateTime.Now,
                    };

                    _context.Add(newRating);
                    _context.SaveChanges();
                }
            }

            return Content("Comment and rating seed succesful!");
        }

    }
}