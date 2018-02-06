using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moviedb.Models
{
    public sealed class Actor
    {
        public Actor()
        {
            Movies = new HashSet<Movie>();
        }



        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ? Dob { get; set; }

        [Display(Name="Biography")]
        public string Bio { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}