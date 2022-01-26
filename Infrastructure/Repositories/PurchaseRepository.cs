using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResultSet<Purchase>> GetAllPurchase(int pageSize = 30, int page = 1)
        {
            var purchaseList = await _dbContext.Purchase.Skip((page-1) * pageSize).Take(pageSize).OrderByDescending(p => p.PurchaseDateTime).ToListAsync();

            var purchaseListCount = await _dbContext.Purchase.CountAsync();

            var pagePurchase = new PagedResultSet<Purchase>(purchaseList, page, pageSize, purchaseListCount);

            return pagePurchase;
        }

    }
}
