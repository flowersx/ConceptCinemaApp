using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Repository.IRepositories
{
    public interface IFoodRepository
    {
        Task<List<FoodAndBeverage>> GetAllAsync();
        Task<FoodAndBeverage> GetByIdAsync(int id);
        Task AddAsync(FoodAndBeverage food);
        Task UpdateAsync(FoodAndBeverage food);
        Task DeleteAsync(int id);
    }
}
