using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moviedb.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}