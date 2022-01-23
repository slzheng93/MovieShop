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
using static ApplicationCore.Models.FavoriteResponseModel;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; IMovieRepository _movieRepository; IPurchaseRepository _purchaseRepository;
        public UserService(IUserRepository userRepository, IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var addFavorite = await _userRepository.AddFavorite(favoriteRequest.UserId, favoriteRequest.MovieId);
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            int movieId = reviewRequest.MovieId;
            int userId = reviewRequest.UserId;
            decimal rating = reviewRequest.Rating;
            string reviewText = reviewRequest?.ReviewText;

            await _userRepository.AddMovieReview(userId, movieId, rating, reviewText);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            //var deletedReview = await _userRepository.DeleteMovieReview(userId, movieId);
            await _userRepository.DeleteMovieReview(userId, movieId);
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            Favorite favorite = await _userRepository.GetFavorite(id, movieId);

            if(favorite == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<FavoriteResponseModel> GetAllFavoriteForUser(int id)
        {
            var favorites = await _userRepository.GetAllFavoritesForUser(id);
            
            var favoriteModel = new List<FavoriteResponseModel>();

            var favoriteMovies = new List<FavoriteMovieResponseModel>();

            foreach (var favorite in favorites)
            {
                favoriteMovies.Add(new FavoriteMovieResponseModel
                {
                    Id = favorite.Id,
                    PosterUrl = favorite.PosterUrl,
                    Title = favorite.Title,
                });
            }
            return new FavoriteResponseModel
            {
                UserId = id,
                FavoriteMovies = favoriteMovies,
            };
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var movieLists = await _userRepository.GetAllPurchasesForUser(id);
            var listOfMovies = new List<MovieDetailsResponseModel>();
            int count = 0;

            foreach (var movie in movieLists)
            {
                listOfMovies.Add(new MovieDetailsResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    Price = movie.Price
                });
                count++;
            }

            return new PurchaseResponseModel
            {
                UserId = id,
                TotalMoviesPurchased = count,
                PurchasedMovies = listOfMovies
            };


        }

        public async Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var reviews = await _userRepository.GetAllReviewsByUser(id);

            var movieReviews = new List<MovieReviewResponseModel>();
            var reviewsModel = new List<UserReviewResponseModel>();

            foreach (var review in reviews)
            {
                movieReviews.Add(new MovieReviewResponseModel
                {
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    Name = review.User.LastName
                });

                
            }
            return new UserReviewResponseModel
            {
                UserId = id,
                MovieReview = movieReviews
            };
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
            decimal price = await _userRepository.GetPriceForMovie(purchaseRequest.MovieId);
            var movieDetail = await _movieRepository.GetById(purchaseRequest.MovieId);

            var newPurchase = new Purchase
            {
                MovieId = movieDetail.Id,
                UserId = userId,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = price,
                PurchaseDateTime = DateTime.Now,
            };


            var dbNewPurchase = await _purchaseRepository.Add(newPurchase);

            if(dbNewPurchase != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

            //var purchase = await _userRepository.AddNewPurchase(userId, purchaseRequest.MovieId, purchaseRequest.TotalPrice);

        public async Task RemoveFavorite(FavoriteRequestModel model)
        {
            await _userRepository.RemoveFavorite(model.UserId, model.MovieId);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var updateReview = await _userRepository.UpdateMovieReview
                (
                    reviewRequest.UserId,
                    reviewRequest.MovieId,
                    reviewRequest.Rating,
                    reviewRequest.ReviewText
                );
        }
        public async Task RemovePurchase(int userId, int movieId)
        {
            await _userRepository.RemovePurchase(userId, movieId);
        }

        public async Task<List<PurchaseDetailsResponseModel>> GetAllPurchaseDetailByUserId(int userId)
        {
            var purchasesDetail = await _userRepository.GetAllPurchaseDetailByUserId(userId);
            var purchasesModel = new List<PurchaseDetailsResponseModel>();

            foreach (var purchase in purchasesDetail)
            {
                purchasesModel.Add(new PurchaseDetailsResponseModel
                {
                    Id = purchase.Id,
                    PurchaseDateTime = purchase.PurchaseDateTime,
                    PurchaseNumber = purchase.PurchaseNumber,
                    TotalPrice = purchase.TotalPrice,
                    UserId = userId,
                });
            }
            return purchasesModel;
            
        }
    }
}