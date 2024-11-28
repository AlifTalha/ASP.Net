using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.DTOs
{
    public class TicketDTO
    {
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Issue is required.")]
        [StringLength(500, ErrorMessage = "Issue cannot exceed 500 characters.")]
        public string Issue { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public bool IsValid()
        {
            ErrorMessages.Clear();

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    ErrorMessages.Add(validationResult.ErrorMessage);
                }
                return false;
            }

            return true; 
        }
    }
}
