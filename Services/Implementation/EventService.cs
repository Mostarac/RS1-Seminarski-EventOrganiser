using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using webapp.Context;
using webapp.Models;
using webapp.Services.Interfaces;
using webapp.ViewModels;

namespace webapp.Services.Implementation
{
    public class EventService : EntityService<Event>, IEventService
    {
        public EventService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Event>();
        }

        public IQueryable<EventVm> GetNewEvents()
        {
            var comments = _context.Comment.ToList();
            var users = _context.Users.ToList();

            var models = _dbSet.Include(x => x.EventType)
                .Include(x => x.Venue)
                .Where(x => x.StartDate > DateTime.Now).Select(x => new EventVm
                {
                    Id = x.Id,
                    Venue = x.Venue.Name,
                    ImageUrl = x.ImageUrl,
                    EventType = x.EventType.Name,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    Description = x.Description,
                    Rating = x.AverageRating ?? 0,
                    CountOfComments = GetCount(comments, x.Id),
                    CreatedBy = GetUser(users, x.CreatedBy)
                }).AsQueryable();


            return models;
        }

        private string GetUser(List<AppUser> users, string createdBy)
        {
            var user = users.FirstOrDefault(x => x.Id == createdBy);
            return user.FirstName + " " + user.LastName;
        }

        private int GetCount(List<Comment> comments, int eventId)
        {
            return comments.Count(x => x.EventId == eventId);
        }

        public EventVm GetVMById(int id)
        {
            return _dbSet.Include(x => x.EventType)
                .Include(x => x.Venue)
                .Where(x => x.Id == id)
                .Select(x => new EventVm
                {
                    Id = x.Id,
                    Venue = x.Venue.Name,
                    ImageUrl = x.ImageUrl,
                    EventType = x.EventType.Name,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Capacity = x.Venue.Capacity,
                    City = x.Venue.Address.City.Name,
                    Country = x.Venue.Address.City.Country.Name,
                    Street = x.Venue.Address.Street, 
                    TicketLink = x.TicketLink
                }).FirstOrDefault();
        }

        public IQueryable<EventVm> GetSimilarEvents(string eventType, int id)
        {
            var similarEvents = _dbSet.Include(x => x.EventType)
                .Include(x => x.Venue)
                .Where(x => x.EventType.Name == eventType && x.Id != id).Select(x => new EventVm
                {
                    Id = x.Id,
                    Venue = x.Venue.Name,
                    ImageUrl = x.ImageUrl,
                    EventType = x.EventType.Name,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    Description = x.Description
                }).AsQueryable().Take(5);

            return similarEvents;
        }
    }
}
