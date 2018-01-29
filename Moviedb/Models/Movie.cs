using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moviedb.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Plot { get; set; }



        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }



        public virtual ICollection<Actor> Actors { get; set; }
    }
}