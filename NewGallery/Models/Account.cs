using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{

    public enum UserType
    {
        Admin,
        Customer
    }
    public class Account
    {

        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Fullname { get; set; }

        public UserType Type { get; set; }
    }
}