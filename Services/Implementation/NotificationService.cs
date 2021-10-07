using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using webapp.Context;
using webapp.Infrastructure;
using webapp.Models;
using webapp.Services.Interfaces;

namespace webapp.Services.Implementation
{
    public class NotificationService : EntityService<Notification>, INotificationService
    {
        private readonly IHubContext<SignalServer> _hubContext;
        private readonly IUserService _userService;
        private readonly IUserNotificationService _userNotificationService;

        public NotificationService(ApplicationDbContext context, IHubContext<SignalServer> hubContext, IUserService userService, IUserNotificationService userNotificationService) : base(context)
        {
            _hubContext = hubContext;
            _userService = userService;
            _userNotificationService = userNotificationService;
            _context = context;
            _dbSet = _context.Set<Notification>();
        }

        public void Create(Notification notification, int eventId)
        {
            Create(notification);

            foreach (var user in _userService.GetAll())
            {
                var userNotification = new NotificationAppUser
                {
                    AppUserId = user.Id,
                    NotificationId = notification.Id
                };

                _userNotificationService.Create(userNotification);
            }

            _hubContext.Clients.All.SendCoreAsync("displayNotification", new []{string.Empty});
        }

        public List<NotificationAppUser> GetUserNotifications(string userId)
        {
            return _context.UserNotification.Where(u => u.AppUserId.Equals(userId) && !u.IsRead)
                .Include(n => n.Notification)
                .ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotification
                .FirstOrDefault(n => n.AppUserId.Equals(userId)
                                     && n.NotificationId == notificationId);
            notification.IsRead = true;
            _context.UserNotification.Update(notification);
            _context.SaveChanges();
        }
    }
}
