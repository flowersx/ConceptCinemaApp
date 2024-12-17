using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Repository.IRepositories
{
    public interface ICinemaHallRepository
    {
        Task<List<CinemaHall>> GetAllAsync();
        Task<CinemaHall> GetByIdAsync(int id);
        Task AddAsync(CinemaHall cinemaHall);
        Task UpdateAsync(CinemaHall cinemaHall);
        Task DeleteAsync(int id);
    }

}
