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

        public async Task<IActionResult> New()
        {
            var viewModel = new MovieFormViewModel
            {
                GenresList = await GetLocalizedGenresDropDownAsync()
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

            var viewModel = new MovieFormViewModel(movieFromDB)
            {
                GenresList = await GetLocalizedGenresDropDownAsync()
            };

            return View("MovieForm", viewModel);
        }

        /* POST Movies/Save
         * To protect from overposting attacks, enable the specific properties you want to bind to, for
         * more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(MovieFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    GenresList = await GetLocalizedGenresDropDownAsync()
                };

                return View("MovieForm", viewModel);
            }


            if (vm.Id == 0)
            {
                var newMovie = Movie.CreateFromViewModel(vm);
                _dbContext.Movies.Add(newMovie);
            }
            else
            {
                var movieInDB = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == vm.Id);
                movieInDB.Name = vm.Name;
                movieInDB.ReleaseDate = vm.ReleaseDate.Value;
                movieInDB.StockNumber = vm.StockNumber.Value;
                movieInDB.GenreId = vm.GenreId.Value;
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetLocalizedGenresDropDownAsync()
        {
            var genresFromDB = await UnitOfWork.Genres.GetAllAsync();

            var localizedItemList = new List<SelectListItem>(genresFromDB.Count);

            var enumerator = genresFromDB.GetEnumerator();
            while (enumerator.MoveNext())
            {
                localizedItemList.Add(new SelectListItem(_stringLocalizer[enumerator.Current.Name], enumerator.Current.Id.ToString()));
            }
            return localizedItemList;
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            UnitOfWork.Dispose();
        }
    }
}
