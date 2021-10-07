using ClientNotifications;
using ClientNotifications.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using webapp.Services.Interfaces;

namespace webapp.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IClientNotification _clientNotification;
        private readonly UserManager<AppUser> _userManager;

        public NotificationsController(INotificationService notificationService,
            UserManager<AppUser> userManager, IClientNotification clientNotification)
        {
            _notificationService = notificationService;
            _userManager = userManager;
            _clientNotification = clientNotification;
        }

        public IActionResult GetNotification()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var notification = _notificationService.GetUserNotifications(userId);
            return Ok(new { UserNotification = notification, notification.Count });
        }

        public IActionResult ReadNotification(int notificationId)
        {
            _notificationService.ReadNotification(notificationId, _userManager.GetUserId(HttpContext.User));

            return Ok();
        }

        public void CreateEventNotification(string name, int id)
        {
            var notification = new Notification
            {
                Text = $"Event '{name}' is created"
            };
            _notificationService.Create(notification, id);

            _clientNotification.AddToastNotification($"Event '{name}' is created",
                NotificationHelper.NotificationType.info,
                new ToastNotificationOption
                {
                    ProgressBar = true,
                    PositionClass = "toast-top-right"
                });
        }
    }
}