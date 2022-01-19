using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        public async Task<IActionResult> Genres(int id)
        {
            var genreCards = await _movieService.GetMovieDetails(id);
            return View(genreCards);
        }
    }
}
