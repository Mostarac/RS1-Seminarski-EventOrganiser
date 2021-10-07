using System.Collections.Generic;
using System.Linq;
using webapp.Context;
using webapp.Models;
using webapp.Services.Interfaces;
using webapp.ViewModels;

namespace webapp.Services.Implementation
{
    public class CommentService : EntityService<Comment>, ICommentService
    {
        public CommentService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Comment>();
        }

        public IEnumerable<CommentViewModel.Row> GetEventComments(int eventId)
        {
            var model = GetAll().Where(x => x.EventId == eventId).OrderByDescending(x => x.CreatedDate);

            var models = model.Select(
                x => new CommentViewModel.Row
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Text = x.Text,
                    Username = x.AppUser.UserName,
                    CreatedDate = x.CreatedDate,
                    EventId = eventId
                }).ToList();

            return models;
        }
    }
}
