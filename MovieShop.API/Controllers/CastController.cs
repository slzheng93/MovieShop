using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCast(int id)
        {
            var cast = await _castService.GetCastDetails(id);

            if (cast != null)
            {
                return Ok(cast);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
