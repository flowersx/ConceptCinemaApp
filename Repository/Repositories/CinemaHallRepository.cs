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
    public class CinemaHallRepository(CinemaDbContext context) : ICinemaHallRepository
    {
        public async Task<List<CinemaHall>> GetAllAsync()
        {
            return await context.CinemaHalls.Include(h => h.Seats).ToListAsync();
        }

        public async Task<CinemaHall> GetByIdAsync(int id)
        {
            return await context.CinemaHalls
                .Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(CinemaHall cinemaHall)
        {
            await context.CinemaHalls.AddAsync(cinemaHall);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CinemaHall cinemaHall)
        {
            context.CinemaHalls.Update(cinemaHall);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hall = await GetByIdAsync(id);
            if (hall != null)
            {
                context.CinemaHalls.Remove(hall);
                await context.SaveChangesAsync();
            }
        }

    }
}
