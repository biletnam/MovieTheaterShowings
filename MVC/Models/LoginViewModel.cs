﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Password")]
        [StringLength(15)]
        public string Password { get; set; }

        //An error message:
        public string errorMessage { get; set; }
    }
}