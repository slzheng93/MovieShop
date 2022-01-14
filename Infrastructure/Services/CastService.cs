using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castService;

        public CastService(ICastRepository castService)
        {
            _castService = castService;
        }
        public async Task<CastDeatilModel> GetCastDetails(int id)
        {
            var castDeatil = await _castService.GetById(id);

            var castModels = new CastDeatilModel
            {
                Id = castDeatil.Id,
                Name = castDeatil.Name,
                Gender = castDeatil.Gender,
                TmdbUrl = castDeatil.TmdbUrl,
                ProfilePath = castDeatil.ProfilePath
            };
            /*
             foreach (var genre in movieDetails.GernesOfMovie)
            {
                movieModel.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }*/

            foreach(var movie in castDeatil.CastOfMovie)
            {
                //castModels.Movies.Add(new CastDeatilModel { Id = movie.CastId, Movies = movie.Movie.Title.ToString()); ;
            }

            return castModels;
        }
    }

}
