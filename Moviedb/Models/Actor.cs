using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moviedb.Models
{
    public class Actor
    {
        public Actor()
        {
            this.Movies = new HashSet<Movie>();
        }



        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}