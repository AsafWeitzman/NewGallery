using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [Display(Name = "Customer Name")]


        public string CustName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Phone Number")]

        public string PhoneNum { get; set; }
    }
}