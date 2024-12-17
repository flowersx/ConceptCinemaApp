using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;

namespace Repository.Repositories
{
    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly CinemaDbContext _context;

        public CinemaHallRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<List<CinemaHall>> GetAllAsync()
        {
            return await _context.CinemaHalls.Include(h => h.Seats).ToListAsync();
        }

        public async Task<CinemaHall> GetByIdAsync(int id)
        {
            return await _context.CinemaHalls
                .Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(CinemaHall cinemaHall)
        {
            await _context.CinemaHalls.AddAsync(cinemaHall);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CinemaHall cinemaHall)
        {
            _context.CinemaHalls.Update(cinemaHall);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hall = await GetByIdAsync(id);
            if (hall != null)
            {
                _context.CinemaHalls.Remove(hall);
                await _context.SaveChangesAsync();
            }
        }

    }
}
