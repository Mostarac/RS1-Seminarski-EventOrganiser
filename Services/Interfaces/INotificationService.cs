using System.Collections.Generic;
using webapp.Models;

namespace webapp.Services.Interfaces
{
    public interface INotificationService : IEntityService<Notification>
    {
        List<NotificationAppUser> GetUserNotifications(string userId);
        void Create(Notification notification, int eventId);
        void ReadNotification(int notificationId, string userId);
    }
}
