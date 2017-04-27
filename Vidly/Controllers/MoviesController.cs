using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController() //Initialize ApplicationDBcontext
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };
            
        //    return View(movie);
        //    //return Content("Hello World!");
        //    //return HttpNotFound();
        //    //return new EmptyResult();
        //    //return RedirectToAction("Index", "Home", new { pageIndex = 1, sortBy = "name" });
        //}

        public ActionResult Index()
        {
            //var movie = new List<Movie>() {new Movie() { Id = 1, Name = "Shrek" },
            //                                 new Movie() { Id = 2, Name = "Spiderman"}};

            var movie = _context.Movie.Include(m => m.Genre).ToList();

            return View(movie);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movie.Where(m => m.Id == id).Include(m => m.Genre).FirstOrDefault();

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movie.Where(m => m.Id == id).Include(m => m.Genre).FirstOrDefault();

            if (movie == null)
                return HttpNotFound();

            var viewModel = new NewMovieViewModel(movie)
            {
                Genre = _context.Genre.ToList()                
            };

            return View("MovieForm",viewModel);
        }

        public ActionResult New()
        {
            var genre = _context.Genre.ToList();

            var viewModel = new NewMovieViewModel()
            {
                Genre = genre
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid) //Adding serverside Validations
            {
                var viewModel = new NewMovieViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Now;
                _context.Movie.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movie.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberOfStock = movie.NumberOfStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}