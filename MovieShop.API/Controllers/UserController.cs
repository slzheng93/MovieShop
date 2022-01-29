using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.API.Helpers;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentLoginUserService _currentUser;
        public UserController(IUserService userService, ICurrentLoginUserService currentLoginUserService)
        {
            _userService = userService;
            _currentUser = currentLoginUserService;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> Purchase(PurchaseRequestModel model)
        {
            var userId = _currentUser.UserId;
            var purchase = await _userService.PurchaseMovie(model, userId);

            if (purchase)
            {
                return Ok(purchase);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite(FavoriteRequestModel model)
        {
            await _userService.AddFavorite(model);
            return Ok(model);
        }
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> UnFavorite(FavoriteRequestModel model)
        {
            await _userService.RemoveFavorite(model);
            return Ok(model);
        }
        [HttpGet]
        [Route("movie/{movieId:int}/favorite")]
        public async Task<IActionResult> FavoriteExist(int movieId)
        {
            var userId = _currentUser.UserId;
            var favoriteMovies = await _userService.FavoriteExists(userId, movieId);

            if (favoriteMovies)
            {
                return Ok(favoriteMovies);
            }
            else
            {
                return NotFound("Not exits");
            }
        }
        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> Review(ReviewRequestModel model)
        {
            await _userService.AddMovieReview(model);
            return Ok(model);
        }
        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel model)
        {
            await _userService.UpdateMovieReview(model);
            return Ok(model);
        }
        [HttpDelete]
        [Route("movie/{movieId}")]
        public async Task<IActionResult> DeletePurchase(int movieId)
        {
            var userId = _currentUser.UserId;
            await _userService.RemovePurchase(userId, movieId);
            return Ok(movieId);
        }
        
        
        [HttpGet]
        [Route("user/{id:int}/purchases")]
        public async Task<IActionResult> GetPurchaseMovies()
        {
            var userId = _currentUser.UserId;
            var userPurchases = await _userService.GetAllPurchasesForUser(userId);
            return Ok(userPurchases);
        }


        [HttpGet]
        [Route("user/{id:int}/{movieId:int}/PurchaseDetailByMovieId")]
        public async Task<IActionResult> GetPurchaseDetailByUserIdMovieId(int movieId)
        {
            var userId = _currentUser.UserId;
            var purchaseDetail = await _userService.GetPurchaseDetails(userId, movieId);
            if(purchaseDetail == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(purchaseDetail);
            }
           
        }
        [HttpGet]
        [Route("user/{id:int}/favorite")]
        public async Task<IActionResult> GetAllFavorite()
        {
            var userId = _currentUser.UserId;
            var favorites = await _userService.GetAllFavoriteForUser(userId);
            return Ok(favorites);
        }
        [HttpGet]
        [Route("user/{id:int}/reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var userId = _currentUser.UserId;
            var favorite = await _userService.GetAllReviewsByUser(userId);
            return Ok(favorite);
        }
        [HttpGet]
        [Route("user/{id:int}/PurchaseDetailByUserId")]
        public async Task<IActionResult> PurchaseDetailsByUserId(int userId)
        {
            var purchases = await _userService.GetAllPurchaseDetailByUserId(userId);

            if (!purchases.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(purchases);
            }
        }
    }
}
