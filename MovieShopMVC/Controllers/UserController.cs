using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        /*--------------------------------------------------
         * Show movies user purchased
         * Show Movies user favorited
         * Purchase/buy a movie
         * Favorite a movie
         * Unfavorite a movie
         * Add movie review
         * Edit movie review
         *--------------------------------------------------
         */

        // ASP.NET has Filters Authorization
       
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // HttpContext => Encapsulates all the Http Request information
           
                var userId = Convert.ToInt32( HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                // call user service with loged in user id and get the movies user purchsed from Purchase table

          
            return View();
        }

       
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            // call user service with loged in user id and get the movies user purchsed from Purchase table


            return View();
        }
    }
}
