﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class GenreResponseModel
    {
        public GenreResponseModel()
        {
            MovieDetails = new List<MovieDetailsResponseModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieDetailsResponseModel> MovieDetails { get; set; }
    }
}