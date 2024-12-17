using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using Repository.IRepositories;
using Services.IServices;
using Services.UnitOfWork;

namespace Services.Services
{
    public class CinemaHallService : ICinemaHallService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CinemaHallService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CinemaHallViewModel>> GetAllAsync()
        {
            var halls = await _unitOfWork.CinemaHallRepository.GetAllAsync();
            return _mapper.Map<List<CinemaHallViewModel>>(halls);
        }

        public async Task<CinemaHallViewModel> GetByIdAsync(int id)
        {
            var hall = await _unitOfWork.CinemaHallRepository.GetByIdAsync(id);
            if (hall == null)
                throw new KeyNotFoundException("Cinema Hall not found.");

            return _mapper.Map<CinemaHallViewModel>(hall);
        }

        public async Task AddAsync(CinemaHallViewModel hallViewModel)
        {
            var cinemaHall = _mapper.Map<CinemaHall>(hallViewModel);

            // Dynamically generate seats
            var seats = GenerateSeats(hallViewModel.Rows, hallViewModel.SeatsPerRow);
            cinemaHall.Seats = seats;
            cinemaHall.TotalSeats = seats.Count;

            await _unitOfWork.CinemaHallRepository.AddAsync(cinemaHall);
        }

        public async Task UpdateAsync(CinemaHallViewModel hallViewModel)
        {
            var hallEntity = await _unitOfWork.CinemaHallRepository.GetByIdAsync(hallViewModel.Id);
            if (hallEntity == null)
                throw new KeyNotFoundException("Cinema Hall not found.");

            // Update basic properties
            hallEntity.Name = hallViewModel.Name;

            // Regenerate seats if rows or seatsPerRow changed
            if (hallEntity.Seats.Count != (hallViewModel.Rows * hallViewModel.SeatsPerRow))
            {
                hallEntity.Seats = GenerateSeats(hallViewModel.Rows, hallViewModel.SeatsPerRow);
                hallEntity.TotalSeats = hallEntity.Seats.Count;
            }

            await _unitOfWork.CinemaHallRepository.UpdateAsync(hallEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.CinemaHallRepository.DeleteAsync(id);
        }

        // Utility method to generate seats dynamically
        private List<Seat> GenerateSeats(int rows, int seatsPerRow)
        {
            var seats = new List<Seat>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < seatsPerRow; j++)
                {
                    seats.Add(new Seat
                    {
                        Row = ((char)('A' + i)).ToString(), // Rows: A, B, C, etc.
                        Number = j + 1,
                        IsAvailable = true
                    });
                }
            }
            return seats;
        }

    }
}
