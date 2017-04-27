using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET   /api/Movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movie.ToList().Select(Mapper.Map<Movie,MovieDto>);
        }

        //GET   /api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movie.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        //POST  /api/Movies/
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movie.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id),movieDto) ;//movieDto;
        }

        //PUT   /api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();//throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movie.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);

            //movieInDb.Name = movie.Name;
            //movieInDb.GenreId = movie.GenreId;
            //movieInDb.AddedDate = movie.AddedDate;
            //movieInDb.ReleaseDate = movie.ReleaseDate;
            //movieInDb.NumberOfStock = movie.NumberOfStock;

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            
            _context.SaveChanges();

            return Ok();
        }

        //DELETE    /api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movie.FirstOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movie.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
