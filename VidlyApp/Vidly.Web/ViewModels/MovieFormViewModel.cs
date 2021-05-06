using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vidly.Web.Models;

namespace Vidly.Web.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            StockNumber = movie.StockNumber;
            GenreId = movie.GenreId;
        }


        public int? Id { get; set; }

        [Required(ErrorMessage = "RequiredMovieName")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredReleaseDate")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "RequiredStockNumber")]
        [Range(1, 20, ErrorMessage = "RangeForStockNumber")]
        public byte? StockNumber { get; set; }

        [Required(ErrorMessage = "RequiredGenre")]
        public byte? GenreId { get; set; }

        public IEnumerable<SelectListItem> GenresList { get; set; }
    }
}
