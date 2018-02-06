using System;
using System.ComponentModel.DataAnnotations;

namespace Moviedb.Models
{
    public class Producer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime ? Dob { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

    }
    
}