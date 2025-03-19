using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepositories;

namespace Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICinemaHallRepository CinemaHallRepository { get; }
        ISeatsRepository SeatsRepository { get; }
        IMovieRepository MovieRepository { get; }
        Task SaveAsync();
    }
}
