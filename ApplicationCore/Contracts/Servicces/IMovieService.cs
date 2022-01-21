using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Servicces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>>GetTop30GrossingMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieCardResponseModel>> MoviesSameGenre (int id);
        Task<PagedResultSet<MovieCardResponseModel>> GetMoviesByPagination(int pageSize, int page, string title);
    }

}
