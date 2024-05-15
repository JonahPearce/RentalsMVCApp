using System.ComponentModel.DataAnnotations;

namespace PROG73020_M3_Group10.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Please enter the current password.")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }
    }
}
