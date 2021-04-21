using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Web.DataAccess;

namespace Vidly.Web.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyDBContext _dbContext;

        public MoviesController(VidlyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var movies = _dbContext.Movies.Include(c => c.Genre);

            return View(movies);
        }

        [Route("movies/details/{id?}")]
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                Id = 0;

            var movie = _dbContext.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == Id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }

        public IActionResult Edit(int id)
        {
            return Content("id = " + id);
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
