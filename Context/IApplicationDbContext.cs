using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<Channel> Channel { get; set; }
        DbSet<City> City { get; set; }
        DbSet<Country> Country { get; set; }
        DbSet<Event> Event { get; set; }
        DbSet<EventChannel> EventChannel { get; set; }
        DbSet<EventType> EventType { get; set; }
        DbSet<Venue> Venue { get; set; }
        DbSet<VenueChannel> VenueChannel { get; set; }
        DbSet<VenueType> VenueType { get; set; }
        DbSet<Wallet> Wallet { get; set; }
        DbSet<TopUp> TopUp { get; set; }
        DbSet<TopUpCard> TopUpCard { get; set; }
        DbSet<GearType> GearType { get; set; }
        DbSet<Gear> Gear { get; set; }
        DbSet<Purchase> Purchase { get; set; }
        DbSet<EventGear> EventGear { get; set; }
        DbSet<Notification> Notification { get; set; }
        DbSet<NotificationAppUser> UserNotification { get; set; }
        DbSet<Comment> Comment { get; set; }
        DbSet<EventRating> EventRating { get; set; }

        DbSet<GearImage> GearImage { get; set; }
        DbSet<ExceptionLog> ExceptionLog { get; set; }
    }
}
