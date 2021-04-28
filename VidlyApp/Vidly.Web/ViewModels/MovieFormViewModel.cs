using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vidly.Web.Models;

namespace Vidly.Web.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }

        public byte SelectedGenre { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}
