using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Vidly.Web.Contracts;
using Vidly.Web.DataAccess;
using Vidly.Web.Models;
using Vidly.Web.ViewModels;

namespace Vidly.Web.Controllers
{
    public class MoviesController : BaseVidlyController
    {
        private VidlyDBContext _dbContext;
        private readonly IStringLocalizer<MoviesController> _stringLocalizer;
        
        public MoviesController(VidlyDBContext dbContext, IProvideUnitOfWork unitOfWork, IStringLocalizer<MoviesController> localizer) : base(unitOfWork)
        {
            _dbContext = dbContext;
            _stringLocalizer = localizer;
        }

        
        public async Task<IActionResult> Index()
        {
            //var movies = _dbContext.Movies.Include(m => m.Genre).AsNoTracking();
            var moviesFromDB = _dbContext.Movies;
            var genresInDB = await UnitOfWork.Genres.GetAllAsync();
            
            foreach(var movie in moviesFromDB)
            {
                foreach(var genre in genresInDB)
                {
                    if (genre.Id != movie.GenreId)
                        continue;
                    movie.Genre.Name = _stringLocalizer[genre.Name];
                }
            }

            return View(moviesFromDB);
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

        public IActionResult New()
        {
            var genresFromDB = UnitOfWork.Genres.GetAll();

            var genresList = new List<SelectListItem>();

            foreach (var genre in genresFromDB)
            {
                string localizedText = _stringLocalizer[genre.Name];
                genresList.Add(new SelectListItem(localizedText, genre.Id.ToString()));
            }

            var viewModel = new MovieFormViewModel
            {
                //Movie = new Movie(),
                Genres = genresList
            };

            return View("MovieForm", viewModel);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (!Id.HasValue)
                return NotFound();

            var movieFromDB = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == Id);
            if (movieFromDB == null)
                return NotFound();

            var genresFromDB = UnitOfWork.Genres.GetAll();
            var genresList = new List<SelectListItem>();

            foreach (var genre in genresFromDB)
            {
                string localizedText = _stringLocalizer[genre.Name];
                var isSelected = movieFromDB.GenreId == genre.Id;
                genresList.Add(new SelectListItem(localizedText, genre.Id.ToString(), isSelected));
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movieFromDB,
                SelectedGenre = movieFromDB.GenreId,
                Genres = genresList
            };

            return View("MovieForm", viewModel);
        }

        /* POST Movies/Save
         * To protect from overposting attacks, enable the specific properties you want to bind to, for
         * more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost]
        public async Task<IActionResult> Save(MovieFormViewModel vm)
        {
            if (vm.Movie.Id == 0)
            {
                vm.Movie.DateAdded = DateTime.Now;
                vm.Movie.GenreId = vm.SelectedGenre;
                _dbContext.Movies.Add(vm.Movie);
            }
            else
            {
                var movieInDB = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == vm.Movie.Id);
                movieInDB.Name = vm.Movie.Name;
                movieInDB.ReleaseDate = vm.Movie.ReleaseDate;
                movieInDB.DateAdded = vm.Movie.DateAdded;
                movieInDB.StockNumber = vm.Movie.StockNumber;
                movieInDB.GenreId = vm.SelectedGenre;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            UnitOfWork.Dispose();
        }
    }
}
