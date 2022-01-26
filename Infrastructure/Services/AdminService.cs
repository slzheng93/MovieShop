using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IPurchaseRepository _purchaseRepository; IMovieRepository _movieRepository;
        public AdminService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository)
        {
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
        }

        public async Task AddMovie(Movie movie)
        {
            movie.CreatedDate = DateTime.Now;
            movie.UpdatedDate = DateTime.Now;
           
            await _movieRepository.Add(movie);
        }

        public async Task<PagedResultSet<Purchase>> GetMoviesByPagination(int pageSize, int page)
        {
            var pagePurchase = await _purchaseRepository.GetAllPurchase(pageSize, page);

            var pagePurchaseLists = new List<Purchase>();

            pagePurchaseLists.AddRange(pagePurchase.Data.Select(p => new Purchase
            {
                Id = p.Id, 
                UserId = p.UserId,
                PurchaseNumber = p.PurchaseNumber,
                TotalPrice = p.TotalPrice,
                PurchaseDateTime = p.PurchaseDateTime,
                MovieId = p.MovieId,
                User = p.User,
                Movie = p.Movie,
            }));

            return new PagedResultSet<Purchase>(pagePurchaseLists,page, pageSize, pagePurchase.Count);
        }
    }
}
