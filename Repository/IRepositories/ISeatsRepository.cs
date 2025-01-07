using DataAccess;

namespace Repository.IRepositories;

public interface ISeatsRepository
{
    Task<List<Seat?>> GetAllAsync();
    Task<Seat?> GetByIdAsync(int id);
    Task AddAsync(Seat cinemaHall);
    Task UpdateAsync(Seat cinemaHall);
    Task DeleteAsync(int id);
    Task<List<Seat>> GetSeatsCinemaHallAsync(int hallId);
    Task AddRangeOfSeatsAsync(List<Seat> seats);
}