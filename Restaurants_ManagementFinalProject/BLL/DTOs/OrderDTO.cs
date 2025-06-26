using System;

namespace BLL.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}