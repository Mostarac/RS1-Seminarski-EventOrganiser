using webapp.Models;

namespace webapp.Services.Interfaces
{
    public interface IEventRatingService : IEntityService<EventRating>
    {
        void AddRating(int rating, string userId, int eventId);
        void UpdateEventRating(int eventId, int rating);
    }
}
