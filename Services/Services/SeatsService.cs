using AutoMapper;
using DataAccess;
using Services.IServices;
using Services.UnitOfWork;

namespace Services.Services;

public class SeatsService(IUnitOfWork unitOfWork, IMapper mapper) : ISeatsService
{
    public Task<List<Seat>> GetAllSeatsForHallAsync(int hallId)
    {
        return unitOfWork.SeatsRepository.GetSeatsCinemaHallAsync(hallId);
    }

    public async Task AddSeatsToHallAsync(int hallId, List<Seat> seats)
    {
        foreach (var seat in seats)
        {
            seat.CinemaHallId = hallId;
            seat.IsAvailable = true;
        }
        
        await unitOfWork.SeatsRepository.AddRangeOfSeatsAsync(seats);
        await unitOfWork.SaveAsync();
    }
}