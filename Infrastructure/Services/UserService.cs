using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FavoriteExists(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<FavoriteResponseModel> GetAllFavoriteForUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDetailsResponseModel> GetPurchaseDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseReques, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }
    }
}
