using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public bool IsSubscribedToNewsletters { get; set;}
        
        public MembershipType MembershipType { get; set; } // Navigation property - Loads both Customer and Membership details in object

        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; } // ForeignKey

        [Display(Name="Date Of Birth")]
        [Min18YearsIfAMember] //Custom Validations
        public DateTime? Birthdate { get; set; } // Can be null
        
    }
}