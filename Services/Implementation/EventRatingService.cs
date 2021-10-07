using System;
using System.Linq;
using webapp.Context;
using webapp.Models;
using webapp.Services.Interfaces;

namespace webapp.Services.Implementation
{
    public class EventRatingService : EntityService<EventRating>, IEventRatingService
    {
        public EventRatingService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EventRating>();
        }
        
        public void AddRating(int rating, string userId, int eventId)
        {
            var model = new EventRating
            {
                CreatedDate = DateTime.Now,
                CreatedBy = userId,
                UserId = userId,
                EventId = eventId,
                Rate = rating
            };

            _context.Add(model);
            _context.SaveChanges();
        }

        public void UpdateEventRating(int eventId, int rating)
        {
            var averageRating = _context.EventRating.Where(x => x.EventId == eventId).Average(x => x.Rate);
            var eventModel = _context.Event.FirstOrDefault(x => x.Id == eventId);
            eventModel.AverageRating = averageRating;
            _context.Event.Update(eventModel);
            _context.SaveChanges();
        }
    }
}
