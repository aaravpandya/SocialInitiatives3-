﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Please Enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string ReturnUrl { get; set; } = "/";
        [Required]
        public string AdmissionNumber { get; set; }
        [Required]
        public string _class { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string House { get; set; }
        
    }
}