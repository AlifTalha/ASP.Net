using System.Collections.Generic;

namespace MeetingMinutesManagement.Models
{
    public class MeetingMinutesViewModel
    {
        public IEnumerable<CorporateCustomer> CorporateCustomers { get; set; }
        public IEnumerable<IndividualCustomer> IndividualCustomers { get; set; }
        public IEnumerable<ProductService> ProductServices { get; set; }
        public MeetingMinutesMaster MeetingMaster { get; set; }
        public List<MeetingMinutesDetails> MeetingDetails { get; set; }
    }
}