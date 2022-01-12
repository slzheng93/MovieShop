using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Here we call MovieService to get topgrossingmovies
            //Three ways we can pass the data from controller to views
            //1. ***Pass the Strongly Typed Models ***
            //2. ViewBag => dynamic
            //3. ViewData => object key/value
            //2 & 3 when we neeed small information when we cant get from 1.
            
            var movies = await _movieService.GetTop30GrossingMovies();
            ViewBag.TotalMovies = movies.Count;
            return View(movies);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        //https://localhost:112498/Home/TopMoviess
        [HttpGet]
        public IActionResult TopMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}