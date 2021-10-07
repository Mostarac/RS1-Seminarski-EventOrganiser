using System.Linq;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Services.Interfaces
{
    public interface IEventService : IEntityService<Event>
    {
        IQueryable<EventVm> GetNewEvents();
        EventVm GetVMById(int id);
        IQueryable<EventVm> GetSimilarEvents(string eventType, int id);
    }
}
