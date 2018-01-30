using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moviedb.Models;


namespace Moviedb.ViewModel
{
    public class MoviesActorViewModel
    {
        public Movie Movie { get; set; }
        public List<Actor> Actors { get; set; }

    }
}