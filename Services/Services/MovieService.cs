using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using Services.IServices;
using Services.UnitOfWork;

namespace Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _unitOfWork.MovieRepository.GetAllAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _unitOfWork.MovieRepository.GetByIdAsync(id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _unitOfWork.MovieRepository.AddAsync(movie);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            await _unitOfWork.MovieRepository.UpdateAsync(movie);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            await _unitOfWork.MovieRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
