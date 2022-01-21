using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> Get30HighestGrossingMovies();
        Task<decimal> GetMovieRating(int id);
        Task<List<Movie>> GetMoviesSameGenre(int id);

        Task<PagedResultSet<Movie>> GetMoviesByTitle(int pageSize = 30, int page = 1, string title = "");
    }
}
