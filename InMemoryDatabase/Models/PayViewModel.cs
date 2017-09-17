using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class PayViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(5)]
        public string PostalCode { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
