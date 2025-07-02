namespace MeetingMinutesManagement.Models
{
    public class MeetingMinutesDetails
    {
        public int MeetingMinutesDetailsId { get; set; }
        public int MeetingMinutesMasterId { get; set; }
        public int ProductServiceId { get; set; }
        public int Quantity { get; set; }
        public ProductService ProductService { get; set; }
    }
}