using ApplicationCore.Contracts.Servicces;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {
        /*---------------------------------------------------------
         * Show cast details along with movies that cast belongs to
         *---------------------------------------------------------
         */
        private readonly ICastService _castService;

        public CastController(ICastService movieService)
        {
            _castService = movieService;
        }

        public async Task<IActionResult> castDetail(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);
            return View(castDetails);
        }
    }
}

