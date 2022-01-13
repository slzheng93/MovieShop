using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<Cast> GetById(int id)
        {
            //we use include method in EF, to navigate and load related data
            
            var cast = await _dbContext.Cast.Include(c => c.CastOfMovie).ThenInclude(c => c.Movie).SingleOrDefaultAsync(c => c.Id == id);   
            
            return cast;
        }
    }
}
