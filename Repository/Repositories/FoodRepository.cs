using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;

namespace Repository.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly CinemaDbContext _context;

        public FoodRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<List<FoodAndBeverage>> GetAllAsync()
        {
            return await _context.FoodAndBeverages.ToListAsync();
        }

        public async Task<FoodAndBeverage> GetByIdAsync(int id)
        {
            return await _context.FoodAndBeverages
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(FoodAndBeverage food)
        {
            await _context.FoodAndBeverages.AddAsync(food);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FoodAndBeverage food)
        {
            _context.FoodAndBeverages.Update(food);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var food = await GetByIdAsync(id);
            if (food != null)
            {
                _context.FoodAndBeverages.Remove(food);
                await _context.SaveChangesAsync();
            }
        }
    }
}
