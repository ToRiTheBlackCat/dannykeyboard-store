using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.User
{
    public class CustomerSignUpRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string OtpCode { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and be 10 digits.")]
        public string Phone { get; set; } = "0";

        public DateOnly? Dob { get; set; }
    }
}
