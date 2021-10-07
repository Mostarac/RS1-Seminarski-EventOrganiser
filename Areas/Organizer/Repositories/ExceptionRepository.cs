using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Context;
using webapp.Models;

namespace webapp.Areas.Organizer.Repositories
{
    public class ExceptionRepository : IExceptionRepository
    {
        private readonly ApplicationDbContext _context;

        public ExceptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ExceptionLog log)
        {
            _context.ExceptionLog.Add(log);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
