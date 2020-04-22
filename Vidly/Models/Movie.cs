
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
/*Data Annotaion
 1. [Required]
 2. [StringLength(255)]
 3. [Range(1, 10)] for numeric type
 4. [Compare("OtherProperty")] to compare to other peoperties. eg. Password and Confirm Password
 5. [Phone]
 6. [EmailAddress]
 7. [Url]
 8. [RegularExpression("..")]
 all these have default validation messages
     */
namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }

        //NumberAvailable = NumberInStock - ActiveRentals
        public byte NumberAvailable { get; set; }
    }
}