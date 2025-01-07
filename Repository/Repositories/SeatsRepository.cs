using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;

namespace Repository.Repositories;

public class SeatsRepository(CinemaDbContext context) : ISeatsRepository
{
    private readonly CinemaDbContext _context = context;
    
    public async Task<List<Seat?>> GetAllAsync()
    {
        return await context.Seats.ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(int id)
    {
        return await context.Seats
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddRangeOfSeatsAsync(List<Seat> seats)
    {
        await _context.Seats.AddRangeAsync(seats);
    }
    
    public async Task<List<Seat>> GetSeatsCinemaHallAsync(int hallId)
    {
      return await  context.Seats.Where(s => s.CinemaHallId == hallId).ToListAsync();
    }
    
    public async Task AddAsync(Seat seat)
    {
        await context.Seats.AddAsync(seat);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seat seat)
    {
        context.Seats.Update(seat);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var seat = await GetByIdAsync(id);
        if (seat != null)
        {
            context.Seats.Remove(seat);
            await context.SaveChangesAsync();
        }
    }
}