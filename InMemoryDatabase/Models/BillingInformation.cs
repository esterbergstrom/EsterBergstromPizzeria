using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryDatabase.Models
{
    public class BillingInformation
    {
        public int BillingInformationId { get; set; }

        public string FullName { get; set; }

        public string StreetAddress { get; set; }

        [StringLength(5)]
        public string PostalCode { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
