using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingMinutesManagement.Models;

namespace MeetingMinutesManagement.Repository
{
    public interface IMeetingMinutesRepository
    {
        Task<IEnumerable<CorporateCustomer>> GetCorporateCustomersAsync();
        Task<IEnumerable<IndividualCustomer>> GetIndividualCustomersAsync();
        Task<IEnumerable<ProductService>> GetProductServicesAsync();
        Task<int> SaveMeetingMinutesMasterAsync(MeetingMinutesMaster master);
        Task SaveMeetingMinutesDetailsAsync(int masterId, IEnumerable<MeetingMinutesDetails> details);
    }
}