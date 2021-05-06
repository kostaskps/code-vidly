using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Web.Models
{
    public class Movie
    {
        public Movie()
        {

        }
        public Movie(ViewModels.MovieFormViewModel viewModel)
        {
            Id = viewModel.Id.Value;
            Name = viewModel.Name;
            ReleaseDate = viewModel.ReleaseDate.Value;
            StockNumber = viewModel.StockNumber.Value;
            GenreId = viewModel.GenreId.Value;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredMovieName")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredReleaseDate")]
        public DateTime ReleaseDate { get; set; }
        
        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "RequiredStockNumber")]
        [Range(1, 20, ErrorMessage = "RangeForStockNumber")]
        public byte StockNumber { get; set; }

        [Required(ErrorMessage = "RequiredGenre")]
        public byte GenreId { get; set; }
        
        public Genre Genre { get; set; }
    }
}
