using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingMinutesManagement.Data;
using MeetingMinutesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingMinutesManagement.Services
{
    public class MeetingMinutesService : IMeetingMinutesService
    {
        private readonly AppDbContext _context;

        public MeetingMinutesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductService> GetProductServiceByIdAsync(int id)
        {
            return await _context.ProductServices.FindAsync(id);
        }
        public async Task<MeetingMinutesViewModel> GetMeetingMinutesViewModelAsync()
        {
            return new MeetingMinutesViewModel
            {
                CorporateCustomers = await _context.CorporateCustomers.ToListAsync(),
                IndividualCustomers = await _context.IndividualCustomers.ToListAsync(),
                ProductServices = await _context.ProductServices.ToListAsync(),
                MeetingMaster = new MeetingMinutesMaster(),
                MeetingDetails = new List<MeetingMinutesDetails>()
            };
        }

        public async Task<MeetingMinutesCreateModel> GetMeetingMinutesCreateModelAsync()
        {
            return new MeetingMinutesCreateModel
            {
                CorporateCustomers = await _context.CorporateCustomers.ToListAsync(),
                IndividualCustomers = await _context.IndividualCustomers.ToListAsync(),
                ProductServices = await _context.ProductServices.ToListAsync()
            };
        }

        public async Task<IEnumerable<CustomerDropdownModel>> GetCustomersByTypeAsync(string customerType)
        {
            if (customerType == "Corporate")
            {
                return await _context.CorporateCustomers
                    .Select(c => new CustomerDropdownModel { Id = c.CorporateCustomerId, Name = c.CustomerName })
                    .ToListAsync();
            }
            else
            {
                return await _context.IndividualCustomers
                    .Select(c => new CustomerDropdownModel { Id = c.IndividualCustomerId, Name = c.CustomerName })
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<ProductService>> GetProductServicesAsync()
        {
            return await _context.ProductServices.ToListAsync();
        }

        public async Task<bool> SaveMeetingMinutesAsync(MeetingMinutesMaster master, int[] productServiceIds, int[] quantities)
        {
            if (productServiceIds == null || productServiceIds.Length == 0)
                return false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Save master record
                    _context.MeetingMinutesMasters.Add(master);
                    await _context.SaveChangesAsync();

                    // Save details records
                    for (int i = 0; i < productServiceIds.Length; i++)
                    {
                        var detail = new MeetingMinutesDetails
                        {
                            MeetingMinutesMasterId = master.MeetingMinutesMasterId,
                            ProductServiceId = productServiceIds[i],
                            Quantity = quantities[i]
                        };
                        _context.MeetingMinutesDetails.Add(detail);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}