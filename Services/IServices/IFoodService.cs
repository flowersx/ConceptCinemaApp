using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Models.ViewModels;

namespace Services.IServices
{
    public interface IFoodService
    {
        Task<List<FoodAndBeverage>> GetAllFoodAsync();
        Task<FoodAndBeverage> GetFoodByIdAsync(int id);
        Task AddFoodAsync(FoodAndBeverage food);
        Task UpdateFoodAsync(FoodAndBeverage food);
        Task DeleteFoodAsync(int id);
    }
}
