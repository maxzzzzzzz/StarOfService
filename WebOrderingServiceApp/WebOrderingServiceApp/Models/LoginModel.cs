﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebOrderingServiceApp.Models
{
    public class LoginModel 
    {

        public string Email { get; set; }
        //[Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}