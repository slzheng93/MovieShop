using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class FavoriteResponseModel
    {
        public FavoriteResponseModel()
        {
            FavoriteMovies = new List<FavoriteMovieResponseModel>();
        }
        public int UserId { get; set; }
        public List<FavoriteMovieResponseModel> FavoriteMovies{ get; set; }

        public class FavoriteMovieResponseModel : MovieCardResponseModel
        {
        }
    }
}
