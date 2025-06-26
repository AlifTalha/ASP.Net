namespace BLL.DTOs
{
    public class TableDTO
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsOccupied { get; set; }
    }
}