namespace BLL.DTOs
{
    public class MenuItemDTO
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsAvailable { get; set; }

        public string ImagePath { get; set; }  // Expose the image path

    }
}