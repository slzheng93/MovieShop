using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
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

        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // ASP.NET has Filters Authorization

        [HttpGet]
        public async Task<IActionResult> Purchased()
        {
            // HttpContext => Encapsulates all the Http Request information
           
            var userId = Convert.ToInt32( HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            // call user service with loged in user id and get the movies user purchsed from Purchase table

            var purchaseDetail = await _userService.GetAllPurchasesForUser(userId);
            
           
            
            return View(purchaseDetail);
        }
        [HttpPost]
        public async Task<IActionResult> BuyAMovie(PurchaseRequestModel model)
        {
            var result = await _userService.IsMoviePurchased(model, model.UserId);

            if (result)
            {
                return RedirectToAction("AlreadyPurchased");
            }
            else
            {
                await _userService.PurchaseMovie(model, model.UserId);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteRequestModel model)
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var result = await _userService.FavoriteExists(userId, model.MovieId);

            if (result)
            {
                return RedirectToAction("AlreadyFavorited");
            }
            else
            {
                await _userService.AddFavorite(model);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UnFavorite(FavoriteRequestModel model)
        {
            var isFavorite = await _userService.FavoriteExists(model.UserId, model.MovieId);
            if (isFavorite)
            {
                await _userService.RemoveFavorite(model);
                return RedirectToAction("FavoriteRemoved");
            }
            else
            {
                return RedirectToAction("NoTFavorite");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Favorited()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            // call user service with loged in user id and get the movies user purchsed from Purchase table
            
            var favoriteDetail = await _userService.GetAllFavoriteForUser(userId);
            
            return View(favoriteDetail);
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var userDetail = await _userService.GetAllReviewsByUser(userId);

            return View(userDetail);
        }
        public async Task<IActionResult> ReviewMovie(ReviewRequestModel model)
        {
            await _userService.AddMovieReview(model);

            return View();
        }
        public async Task<IActionResult> AlreadyPurchased()
        {
            return View();
        }
        public async Task<IActionResult> AlreadyFavorited()
        {
            return View();
        }
        public async Task<IActionResult> FavoriteRemoved()
        {
            return View();
        }
        public async Task<IActionResult> NoTFavorite()
        {
            return View();
        }
    }
}
