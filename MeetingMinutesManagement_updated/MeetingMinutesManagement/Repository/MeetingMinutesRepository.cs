using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingMinutesManagement.Data;
using MeetingMinutesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingMinutesManagement.Repository
{
    public class MeetingMinutesRepository : IMeetingMinutesRepository
    {
        private readonly AppDbContext _context;

        public MeetingMinutesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CorporateCustomer>> GetCorporateCustomersAsync()
        {
            return await _context.CorporateCustomers.ToListAsync();
        }

        public async Task<IEnumerable<IndividualCustomer>> GetIndividualCustomersAsync()
        {
            return await _context.IndividualCustomers.ToListAsync();
        }

        public async Task<IEnumerable<ProductService>> GetProductServicesAsync()
        {
            return await _context.ProductServices.ToListAsync();
        }

        public async Task<int> SaveMeetingMinutesMasterAsync(MeetingMinutesMaster master)
        {
            _context.MeetingMinutesMasters.Add(master);
            await _context.SaveChangesAsync();
            return master.MeetingMinutesMasterId;
        }

        public async Task SaveMeetingMinutesDetailsAsync(int masterId, IEnumerable<MeetingMinutesDetails> details)
        {
            _context.MeetingMinutesDetails.AddRange(details);
            await _context.SaveChangesAsync();
        }
    }
}