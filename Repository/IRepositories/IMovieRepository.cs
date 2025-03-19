using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Repository.IRepositories
{
    public interface IMovieRepository
    {
        Task DeleteAsync(int id);
        Task UpdateAsync(Movie movie);
        Task AddAsync(Movie movie);
        Task<Movie?> GetByIdAsync(int id);
        Task<List<Movie>> GetAllAsync();
    }
}
