using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class NotificationAppUser
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
