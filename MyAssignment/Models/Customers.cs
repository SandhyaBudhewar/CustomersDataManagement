using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAssignment.Models
{
    public partial class Customers
    {
        [Key]
        public string CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PaymentCategory { get; set; }

        [Required]
        public string Phone { get; set; }
       
    }
}
