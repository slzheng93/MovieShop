using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>,IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {
        }

        public async Task<List<Movie>> Get30HighestGrossingMovies()
        {
            //here we use LINQ && EF Core to return movies from database
            //EF Core does the I/O bound operation
            //EF Core has both async and sync method
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();

            return movies;
        }
        public override async Task<Movie> GetById(int id) 
        {
            //we use include method in EF, to navigate and load related data
            var movie = await _dbContext.Movies.Include(m => m.Trailers).Include(m => m.MovieCast).ThenInclude(m => m.Cast)
                .Include(m => m.GernesOfMovie).ThenInclude(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            return movie;
        }
        public async Task<decimal> GetMovieRating(int id)
        {
            var movieRating = await _dbContext.Review.Where(r => r.MovieId == id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);
            return movieRating;
        }

        public async Task<List<Movie>> GetMoviesSameGenre(int id)
        {
            var genreMovies = await _dbContext.MovieGenres.Include(m => m.Movie).Where(m => m.GenreId == id).Select(m => m.Movie).ToListAsync();
            return genreMovies;
        }
    }
}