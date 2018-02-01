using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moviedb.Models
{
    public class Movie
    {
        public Movie()
        {
           this.Actors = new HashSet<Actor>();
            
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public string Plot { get; set; }


        public string MoviePosterPath { get; set; }


        [Display(Name = "Producer")]
        public int ProducerId { get; set; }
       
        public virtual Producer Producer { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}