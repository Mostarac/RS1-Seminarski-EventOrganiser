using System.Threading.Tasks;
using webapp.Context;
using webapp.Models;
using webapp.Services.Interfaces;
using webapp.ViewModels;

namespace webapp.Services.Implementation
{
    public class UserService : EntityService<AppUser>, IUserService
    {
        public UserService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<AppUser>();
        }

        public async Task<UserVm> GetByIdAsync(string id)
        {
            var user = await GetByIdAsync((object)id);

            if (user == null) return new UserVm();

            return new UserVm
            {
                Id = user.Id,
                CityId = user.CityId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Username = user.UserName,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash
            };
        }
    }
}
