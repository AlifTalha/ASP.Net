using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        [Required]
        public DateTime OrderTime { get; set; } = DateTime.Now;

        [Required]
        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; } = false;
    }
}