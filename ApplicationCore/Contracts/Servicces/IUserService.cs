using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Servicces
{
    public interface IUserService
    {
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseReques, int userId);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task<PurchaseDetailsResponseModel> GetPurchaseDetails(int userId, int movieId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task RemovePurchase(int userId, int movieId);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<FavoriteResponseModel> GetAllFavoriteForUser(int id);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<UserReviewResponseModel> GetAllReviewsByUser(int id);
        Task<List<PurchaseDetailsResponseModel>> GetAllPurchaseDetailByUserId(int userId);



    }
}
