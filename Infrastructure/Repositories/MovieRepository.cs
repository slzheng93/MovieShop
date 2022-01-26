using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task GenerateNewMovie (AdminMovieRequestModel movie)
        {

            List<GenreModel> genres = new List<GenreModel>();

            foreach (var item in movie.Genres)
            {
                genres.Add(new GenreModel { Name = item.Name});
            }

            var movieModel = new Movie
            {
                Title = movie.Title,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Revenue = movie.Revenue,
                Budget = movie.Budget,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                UpdatedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
            };

            await _dbContext.Movies.AddAsync(movieModel);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateMovie(AdminMovieRequestModel movie)
        {
            var updateMovie = await _dbContext.Movies.Where(m => m.Title == movie.Title).SingleOrDefaultAsync();

            if (updateMovie == null)
            {
                await GenerateNewMovie(movie);
            }
            else
            {
                updateMovie.Overview = movie.Overview;
                updateMovie.Tagline = movie.Tagline;
                updateMovie.Revenue = movie.Revenue;
                updateMovie.Budget = movie.Budget;
                updateMovie.ImdbUrl = movie.ImdbUrl;
                updateMovie.TmdbUrl = movie.TmdbUrl;
                updateMovie.PosterUrl = movie.PosterUrl;
                updateMovie.BackdropUrl = movie.BackdropUrl;
                updateMovie.OriginalLanguage = movie.OriginalLanguage;
                updateMovie.ReleaseDate = movie.ReleaseDate;
                updateMovie.RunTime = movie.RunTime;
                updateMovie.Price = movie.Price;
                updateMovie.UpdatedDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }

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

        public async Task<PagedResultSet<Movie>> GetMoviesByTitle(int pageSize = 30, int page = 1, string title = "")
        {
            // select * from Movies where title like '%ave%' order by title offset 0 fetch next rows 30;
            var movies = await _dbContext.Movies.Where(m => m.Title.Contains(title)).OrderBy(m => m.Title).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // total movies  for that condition 
            // select count(*) from Movies where title like '%ave%'

            var totalMoviesCount = await _dbContext.Movies.Where(m => m.Title.Contains(title)).CountAsync();

            var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCount);

            return pagedMovies;

        }

        public async Task<List<Movie>> GetMoviesSameGenre(int id)
        {
            var genreMovies = await _dbContext.MovieGenres.Include(m => m.Movie).Where(m => m.GenreId == id).Select(m => m.Movie).ToListAsync();
            return genreMovies;
        }

        public async Task<List<Movie>> Get30HighestRatedMovies()
        {
            var topRatedMovies = await _dbContext.Review.Include(r => r.Movie).GroupBy(r => new { r.MovieId, r.Movie.PosterUrl, r.Movie.Title })
                .OrderByDescending(m => m.Average(r => r.Rating)).Select(m => new Movie
                {
                    Id = m.Key.MovieId,
                    PosterUrl = m.Key.PosterUrl,
                    Title = m.Key.Title,
                }).Take(30).ToListAsync();

            return topRatedMovies;
        }
    }
}
