using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Areas.Organizer.Repositories
{
    public interface IExceptionRepository
    {
        void Add(ExceptionLog log);
        void Save();
    }
}
