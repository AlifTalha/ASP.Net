using System;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.DTOs
{
   
    public class RegistrationDTO
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters long.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; } 
    }

}
