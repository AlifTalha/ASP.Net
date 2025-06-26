using System.ComponentModel.DataAnnotations;

namespace DAL.EF
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } // In production: Hash this!
    }
}
