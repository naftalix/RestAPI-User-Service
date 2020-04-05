using System;
using UserManagment.Persistence.Context;

namespace UserManagment.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly UserManagmentContext _context;

        public BaseRepository(UserManagmentContext context)
        {
            _context = context;
        }
    }
}
