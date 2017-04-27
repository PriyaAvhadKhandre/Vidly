using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class NewMovieViewModel
    {
        public int? Id { get; set; }
        public IEnumerable<Genre> Genre { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }
        [Display(Name = "Release Date")]

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberOfStock { get; set; }

        public string Title
        {
            get
            {
                //if (Movie != null && Movie.Id != 0)
                //    return "Edit Movie";

                //return "New Movie";

                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public NewMovieViewModel()
        {
            Id = 0;
        }

        public NewMovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberOfStock = movie.NumberOfStock;
            GenreId = movie.GenreId;
        }
    }
}