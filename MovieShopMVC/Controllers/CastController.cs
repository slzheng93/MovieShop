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

        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        public async Task<IActionResult> castDetails(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);
            return View(castDetails);
        }
    }
}

