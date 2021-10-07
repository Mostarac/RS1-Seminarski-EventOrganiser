using System.Threading.Tasks;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Services.Interfaces
{
    public interface IUserService : IEntityService<AppUser>
    {
        Task<UserVm> GetByIdAsync(string id);
    }
}
