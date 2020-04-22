using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        //These rules are only applied on the server-side not on the client-side.
        //To apply these rules, you need to write them using JQuery seperately.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //ObjectInstance gives access to Containing Class
            //ObjectInstance is an object therefore cast it to customer class type
            var customer = (Customer)validationContext.ObjectInstance;
            if(customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if(customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
            // get the difference in years
            //var age = DateTime.Now.Year - customer.Birthdate.Value.Year;
            // subtract another year if we're before the
            // birth day in the current year
            //if (DateTime.Now.Month < customer.Birthdate.Value.Month || 
            //(DateTime.Now.Month == customer.Birthdate.Value.Month && 
            //DateTime.Now.Day < customer.Birthdate.Value.Day))
                //age--;
            return (age > 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be atleast 18 years old to go on a membership.");
        }
    }
}