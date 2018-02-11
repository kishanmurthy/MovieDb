using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Dob { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}