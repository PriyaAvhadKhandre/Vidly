using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletters { get; set; }

        public byte MembershipTypeId { get; set; } // ForeignKey

        //[Min18YearsIfAMember] //Custom Validations // Comment when using IHttpActionResult
        public DateTime? Birthdate { get; set; } // Can be null
    }
}