using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;
/* We reomved the MembershipType property, because this is a domain class. As it is creating dependency 
    to out Dto from the domain model. If we change the MembershipType, it will affect our Dto, so either we 
    use primitive type(byte, int, etc.) or custom Dtos.*/
/*So we want to return hierarchical data structures, so we will create another type called MembershipType
    Dto. This way our Dtos arec ompletely de-coupled from domain objects.*/
//We also don't need Display attributes as we already used them.
namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        //Override the default validation message attribute
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipTypeDto MembershipType { get; set; }

        //MembershipTypeId is required even when we have not given it the required annotation.
        //This is because it is of byte type. To make it optional add ? after byte without giving a space.
        //This is beacuse, the Select Membership Type does not have a value. Therefore, it will be an empty string.
        //MVC does not know how to convert empty string to byte.

        public byte MembershipTypeId { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}