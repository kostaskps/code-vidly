using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Web.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        public int StockNumber { get; set; }
        public byte GenreId { get; set; }
        public Genres Genre { get; set; }
    }
}
