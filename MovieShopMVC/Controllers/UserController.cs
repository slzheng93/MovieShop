﻿using ApplicationCore.Contracts.Servicces;
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
        [HttpGet]
        public async Task<IActionResult> Favorited()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            // call user service with loged in user id and get the movies user purchsed from Purchase table
            
            var favoriteDetail = await _userService.GetAllFavoriteForUser(userId);

            return View();
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
                var purchase = _userService.PurchaseMovie(model, model.UserId);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ReviewMovie(ReviewRequestModel model)
        {
            await _userService.AddMovieReview(model);

            return View();
        }
        public async Task<IActionResult> AlreadyPurchased()
        {
            return View();
        }
    }
}
