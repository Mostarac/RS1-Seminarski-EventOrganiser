using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public List<NotificationAppUser> NotificationAppUsers { get; set; }
    }
}
