using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Web.Models;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            var movies = ProvideMovies();

            return View(movies);
        }
        public IActionResult Random()
        {
            var movie = new Models.Movie() { Name = "Shrek!" };
            var customers = new List<Customer> 
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };
            var viewModel = new RandomMovieViewModel 
            { 
                Movie = movie, 
                Customers = customers 
            };

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return Content("id = " + id);
        }

        

        [Route("movies/released/{year}/{month:regex(\\d{{2}}):range(1,12)}")]
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        private IEnumerable<Movie> ProvideMovies()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek!"},
                new Movie { Id = 2, Name = "Wall-e"}
            };
            return movies;
        }

    }
}
