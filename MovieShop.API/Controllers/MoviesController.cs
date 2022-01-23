using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService; IUserService _userService;
        public MoviesController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }


        [HttpGet]
        [Route("")]
        // http://localhost:73434/api/movies?pagesize=30&page=2&title=ave
        public async Task<IActionResult> GetMoviesByPagination([FromQuery] int pageSize = 30, [FromQuery] int page =1, string  title= "")
        {
            var movies = await _movieService.GetMoviesByPagination(pageSize, page, title);
            if (movies == null || movies.Count == 0)
            {
                return NotFound($"no movies found for your search term {title}");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30GrossingMovies();

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if(movie == null)
                return NotFound();
            return Ok(movie);
        }
        [HttpGet]
        [Route("Top30Movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetTop30GrossingMovies();

            if(!movies.Any() || movies.Count == 0)
            {
                return NotFound();
            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("genre/{id:int}")] 
        public async Task<IActionResult> GetMovieByGenreId(int id)
        {
            var genreMovies = await _movieService.MoviesSameGenre(id);

            if (!genreMovies.Any())
            {
                return NotFound();
            }
            return Ok(genreMovies);
        }
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(int id)
        {
            var userMovieReviews = await _userService.GetAllReviewsByUser(id);
            
            if(userMovieReviews == null)
            {
                return NotFound();
            }
            return Ok(userMovieReviews);
        }
    }
}
