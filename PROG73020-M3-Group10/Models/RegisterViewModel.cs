﻿using System.ComponentModel.DataAnnotations;

namespace PROG73020_M3_Group10.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Your email should follow a valid email address format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone(ErrorMessage = "Your Phone number should be in a valid format.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public string? LastName { get; set; }
    }
}