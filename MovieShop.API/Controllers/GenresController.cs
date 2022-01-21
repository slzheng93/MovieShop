using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService; 
        }

        // api/genres/
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllGenres();
            //return JSON 
            //Serialize C# to JSON
            //NewtonSoft.JSON (ASP.NET Core before 3)
            //After 3: System.Text.JSON => Microsoft
            //API, always return HTTP status code along with data

            if (!genres.Any()) //whether its empty
            {
                return NotFound();
            }

            //200
            return Ok(genres);
        }
    }
}
