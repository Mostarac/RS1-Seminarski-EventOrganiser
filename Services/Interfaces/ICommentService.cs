using System.Collections.Generic;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Services.Interfaces
{
    public interface ICommentService : IEntityService<Comment>
    {
        IEnumerable<CommentViewModel.Row> GetEventComments(int eventId);
    }
}
