using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Favorite> AddFavorite(int userId, int movieId)
        {
            var favorited = await _dbContext.Favorites.Where(f => f.UserId == userId && f.MovieId ==movieId).SingleOrDefaultAsync();

            if(favorited == null)
            {
                var newFavorite = new Favorite { UserId = userId, MovieId = movieId };
                await _dbContext.Favorites.AddAsync(newFavorite);
                await _dbContext.SaveChangesAsync();

                return newFavorite;
            }

            return favorited;
        }

        public async Task<Review> AddMovieReview(int userId, int movieId, decimal rating, string text)
        {
            if(rating > 5)
            {
                rating = 5;
            }
            var MovieReview = await _dbContext.Review.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefaultAsync();
            
            if(MovieReview == null)
            {
                var newReview = new Review { UserId = userId,MovieId = movieId,Rating = rating,ReviewText=text };
                await _dbContext.Review.AddAsync(newReview);
                await _dbContext.SaveChangesAsync();
                return newReview;
            }
            else
            {
                var newReview = await UpdateMovieReview(userId, movieId, rating, text);
                return newReview;
            }
        }

        public async Task<Review> DeleteMovieReview(int userId, int movieId)
        {
            var moviewReview = await _dbContext.Review.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefaultAsync();

            if(moviewReview == null)
            {
                return null;
            }
            else
            {
                _dbContext.Review.Remove(moviewReview);
                await _dbContext.SaveChangesAsync();
                return moviewReview;
            }
        }

        public async Task<List<Movie>> GetAllFavoritesForUser(int id)
        {
            var favoriteMovies = await _dbContext.Favorites.Include(f => f.Movie).Where(f => f.UserId == id).Select(f => f.Movie).ToListAsync();
            

            return favoriteMovies;
        }

        public async Task<List<Movie>> GetAllPurchasesForUser(int userId)
        {
            var movies = await _dbContext.Purchase.Include(p => p.Movie).Where(p => p.UserId == userId).Select(p => p.Movie).ToListAsync();

            return movies;
        }

        public async Task<List<Review>> GetAllReviewsByUser(int id)
        {
            var reviews = await _dbContext.Review.Include(r => r.User).Where(r => r.UserId == id).ToListAsync();

            return reviews; 
        }

        public async Task<Favorite> GetFavorite(int userId, int movieId)
        {
            var favorite = await _dbContext.Favorites.Where(f => f.UserId == userId && f.MovieId == movieId).SingleOrDefaultAsync();

            return favorite;
        }

        public async Task<decimal> GetPriceForMovie(int movieId)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            return movie.Price.GetValueOrDefault();
        }

        public async Task<Purchase> GetPurchasesDetails(int userId, int movieId)
        {
            var purchaseDetail = await _dbContext.Purchase.Where(p => p.UserId == userId && p.MovieId == movieId).SingleOrDefaultAsync();

            return purchaseDetail;
        }

        public async Task<Review> GetReviewByUserAndMovie(int userId, int movieId)
        {
            var review = await _dbContext.Review.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefaultAsync();

            return review;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<Favorite> RemoveFavorite(int userId, int movieId)
        {
            var favorite = await _dbContext.Favorites.Where(f => f.UserId == userId && f.MovieId == movieId).SingleOrDefaultAsync();

            if(favorite != null)
            {
                _dbContext.Favorites.Remove(favorite);
                await _dbContext.SaveChangesAsync();
                return favorite;
            }
            return null;
        }
        public async Task<Purchase> RemovePurchase(int userId, int MovieId)
        {
            var movie = await _dbContext.Purchase.Where(p => p.UserId == userId && p.MovieId == MovieId).SingleOrDefaultAsync();

            if (movie != null)
            {
                _dbContext.Purchase.Remove(movie);
                await _dbContext.SaveChangesAsync();
                return movie;
            }
            return null;
        }


        public async Task<Review> UpdateMovieReview(int userId, int movieId, decimal rating, string text)
        {
            var updateReview = await _dbContext.Review.Where(ur => ur.UserId == userId && ur.MovieId == movieId).SingleOrDefaultAsync();

            if (updateReview == null)
            {
                var newReview = await AddMovieReview(userId, movieId, rating, text);
                return newReview;
            }          
            else
            {

                //var result = await _dbContext.Review.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefaultAsync();
                
                updateReview.Rating = rating;
                updateReview.ReviewText = text;
                //await AddMovieReview(userId,movieId,updateReview.Rating,updateReview.ReviewText);
                await _dbContext.SaveChangesAsync();

                return updateReview;
            }
        }

        public async Task<List<Purchase>> GetAllPurchaseDetailByUserId(int id)
        {
            var purchase = await _dbContext.Purchase.Where(p => p.UserId == id).ToListAsync();

            return purchase;
        }
    }
}
