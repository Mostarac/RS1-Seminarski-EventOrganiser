using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using ClientNotifications;
using ClientNotifications.Helpers;
using Microsoft.AspNetCore.Authorization;
using webapp.Models;
using webapp.Services.Interfaces;

namespace webapp.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService, INotificationService notificationService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            var events = _eventService.GetNewEvents();

            return View(events);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
