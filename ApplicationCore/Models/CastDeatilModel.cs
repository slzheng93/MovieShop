using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CastDeatilModel
    {
        public CastDeatilModel()
        {
            MovieTitle = new List<MovieCardResponseModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }
        public List<MovieCardResponseModel> MovieTitle { get; set; }

    }
}
