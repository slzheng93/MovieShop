using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository; IAdminService _adminService; IMovieRepository _movieRepository;

        public AdminController(IPurchaseRepository purchaseRepository, IAdminService adminService, IMovieRepository movieRepository)
        {
            _purchaseRepository = purchaseRepository;
            _adminService = adminService;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPurchasesByPagination([FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var purchaseList = await _adminService.GetMoviesByPagination(pageSize, page);

            if(purchaseList == null || purchaseList.Count == 0)
            {
                return NotFound("No Purchase Found");
            }
            return Ok(purchaseList);
        }
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddMovie(AdminMovieRequestModel movie)
        {
            await _movieRepository.GenerateNewMovie(movie);
            return  Ok(movie);
        }
        
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> EditMovie(AdminMovieRequestModel movie)
        {
            await _movieRepository.UpdateMovie(movie);
            return Ok(movie);
        }
    }
}
