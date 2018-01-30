using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public DateTime ? DOB { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

    }
    
}