using DataAccess;
using Models.ViewModels;

namespace Services.IServices
{
    public interface ICinemaHallService
    {
        Task<List<CinemaHallViewModel>> GetAllAsync();
        Task<CinemaHallViewModel> GetByIdAsync(int id);
        Task AddAsync(CinemaHallViewModel hallViewModel);
        Task UpdateAsync(CinemaHallViewModel hallViewModel);
        Task DeleteAsync(int id);

    }
}


