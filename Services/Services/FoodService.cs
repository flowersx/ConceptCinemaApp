using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using Repository.IRepositories;
using Services.IServices;
using Services.UnitOfWork;

namespace Services.Services
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FoodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<FoodAndBeverage>> GetAllFoodAsync()
        {
            return await _unitOfWork.FoodRepository.GetAllAsync();
        }

        public async Task<FoodAndBeverage> GetFoodByIdAsync(int id)
        {
            return await _unitOfWork.FoodRepository.GetByIdAsync(id);
        }

        public async Task AddFoodAsync(FoodAndBeverage food)
        {
            await _unitOfWork.FoodRepository.AddAsync(food);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateFoodAsync(FoodAndBeverage food)
        {
            await _unitOfWork.FoodRepository.UpdateAsync(food);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFoodAsync(int id)
        {
            await _unitOfWork.FoodRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
