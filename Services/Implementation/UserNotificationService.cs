using webapp.Context;
using webapp.Models;
using webapp.Services.Interfaces;

namespace webapp.Services.Implementation
{
    public class UserNotificationService : EntityService<NotificationAppUser>, IUserNotificationService
    {
        public UserNotificationService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<NotificationAppUser>();
        }
    }
}
