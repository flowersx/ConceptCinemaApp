using DataAccess;

namespace Services.IServices;

public interface ISeatsService
{
    Task<List<Seat>> GetAllSeatsForHallAsync(int hallId);
    Task AddSeatsToHallAsync(int hallId,  List<Seat> seats);
}