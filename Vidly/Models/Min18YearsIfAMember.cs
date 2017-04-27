using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
 	         var cusomter = (Customer) validationContext.ObjectInstance;

            if(cusomter.MembershipTypeId == MembershipType.PayAsYouGo 
            || cusomter.MembershipTypeId == MembershipType.Unknown)
                return ValidationResult.Success;

            if(cusomter.Birthdate == null)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - cusomter.Birthdate.Value.Year;

            return (age >= 18) 
                        ? ValidationResult.Success 
                        : new ValidationResult("Customer should atleast be 18 years old to go on a membership.");
        }
    }
}