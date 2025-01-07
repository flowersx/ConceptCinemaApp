using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Repository.IRepositories;
using Repository.Repositories;

namespace Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext _context;

        public UnitOfWork(CinemaDbContext context)
        {
            _context = context;
        }

        public ICinemaHallRepository CinemaHallRepository => new CinemaHallRepository(_context);
        public ISeatsRepository SeatsRepository => new SeatsRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
