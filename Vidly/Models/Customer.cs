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
    public class Customer
    {
        public int Id { get; set; }
        //Override the default validation message attribute
        [Required(ErrorMessage = "Please enter Customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        //MembershipTypeId is required even when we have not given it the required annotation.
        //This is because it is of byte type. To make it optional add ? after byte without giving a space.
        //This is beacuse, the Select Membership Type does not have a value. Therefore, it will be an empty string.
        //MVC does not know how to convert empty string to byte.
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}