using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesConroller : Controller
    {
        /*--------------------------------------------------
         * Details
         * TopRatedMovies
         *--------------------------------------------------
         */
        private readonly IMovieService _movieService;
        public MoviesConroller(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }
    }
}
