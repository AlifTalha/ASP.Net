using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingMinutesManagement.Models;

namespace MeetingMinutesManagement.Services
{
    public interface IMeetingMinutesService
    {
        Task<ProductService> GetProductServiceByIdAsync(int id);
        Task<MeetingMinutesViewModel> GetMeetingMinutesViewModelAsync();
        Task<IEnumerable<CustomerDropdownModel>> GetCustomersByTypeAsync(string customerType);
        Task<IEnumerable<ProductService>> GetProductServicesAsync();
        Task<bool> SaveMeetingMinutesAsync(MeetingMinutesMaster master, int[] productServiceIds, int[] quantities);
    }

    public class CustomerDropdownModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}