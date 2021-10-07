using webapp.Context;
using webapp.Models;
using webapp.Services.Interfaces;

namespace webapp.Services.Implementation
{
    public class CityService : EntityService<City>, ICityService
    {
        public CityService(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<City>();
        }
    }
}