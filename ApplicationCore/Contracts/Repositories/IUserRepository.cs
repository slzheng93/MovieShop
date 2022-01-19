using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<Review> GetReviewByUserAndMovie(int userId, int movieId);
        Task<List<Movie>> GetAllMoviesPurchasedByUser(int userId);
        Task<Purchase> AddNewPurchase(int userId, int movieId, decimal price);
        Task<Favorite> GetFavorite(int userId, int movieId);


        Task<List<Purchase>> GetAllPurchasesForUser(int id);
        Task<Purchase> GetPurchasesDetails(int userId, int movieId);
        Task<List<Movie>> GetAllFavoritesForUser(int id);
        Task<Favorite> AddFavorite(int userId, int movieId);
        Task<Favorite> RemoveFavorite(int userId, int movieId);
        Task<List<Review>> GetAllReviewsByUser(int id);
        Task<Review> AddMovieReview(int userId, int movieId, decimal rating, string text);
        Task<Review> UpdateMovieReview(int userId, int movieId, decimal rating, string text);
        Task<Review> DeleteMovieReview(int userId, int movieId);
    }
}
