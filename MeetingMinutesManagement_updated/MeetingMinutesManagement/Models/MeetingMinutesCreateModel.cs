using System.Collections.Generic;

namespace MeetingMinutesManagement.Models
{
    public class MeetingMinutesCreateModel
    {
        public IEnumerable<CorporateCustomer> CorporateCustomers { get; set; }
        public IEnumerable<IndividualCustomer> IndividualCustomers { get; set; }
        public IEnumerable<ProductService> ProductServices { get; set; }
    }
}