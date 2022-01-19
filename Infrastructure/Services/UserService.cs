using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            int userId = favoriteRequest.UserId;
            int movieId = favoriteRequest.MovieId;
            var addFavorite = await _userRepository.AddFavorite(userId, movieId);
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            int movieId = reviewRequest.MovieId;
            int userId = reviewRequest.UserId;
            decimal rating = reviewRequest.Rating;
            string reviewText = reviewRequest?.ReviewText;
            var addMovieReview = await _userRepository.AddMovieReview(userId, movieId, rating, reviewText);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            //var deletedReview = await _userRepository.DeleteMovieReview(userId, movieId);
            await _userRepository.DeleteMovieReview(userId, movieId);
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            Favorite favorite = await _userRepository.GetFavorite(id, movieId);

            return favorite != null;
        }
        //INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE
        public async Task<FavoriteResponseModel> GetAllFavoriteForUser(int id)
        {
            var favorite = await _userRepository.GetAllFavoritesForUser(id);

            var favoriteModel = new FavoriteResponseModel();

            return favoriteModel;
        }
        //INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE
        public Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchaseUser = _userRepository.GetAllPurchasesForUser(id);

            var purchaseModel = new PurchaseResponseModel();

            return null;
            //return purchaseModel;
        }
        //INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE//INCOMPLETE
        public Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchaseDetails(int userId, int movieId)
        {
            var result = await _userRepository.GetPurchasesDetails(userId, movieId);

            var resultModel = new PurchaseDetailsResponseModel
            {
                Id = result.Id,
                UserId = result.UserId,
                PurchaseNumber = result.PurchaseNumber,
                PurchaseDateTime = result.PurchaseDateTime,
                TotalPrice = result.TotalPrice,
            };

            return resultModel;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseReques, int userId)
        {
            var isPurchased = await _userRepository.GetPurchasesDetails(userId, purchaseReques.MovieId);

            return isPurchased != null;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = await _userRepository.AddNewPurchase(userId, purchaseRequest.MovieId, purchaseRequest.TotalPrice);

            return purchase != null;
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var remove = await _userRepository.RemoveFavorite(favoriteRequest.UserId, favoriteRequest.MovieId);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var updateReview = await _userRepository.UpdateMovieReview
                (
                    reviewRequest.MovieId,
                    reviewRequest.UserId,
                    reviewRequest.Rating,
                    reviewRequest.ReviewText
                );
        }
    }
}
