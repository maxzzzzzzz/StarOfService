using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebOrderingServiceApp.Models
{
    public class RegisterModel
    {
        public string Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }



        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}