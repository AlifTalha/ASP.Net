using Microsoft.EntityFrameworkCore;
using MeetingMinutesManagement.Models;

namespace MeetingMinutesManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<MeetingMinutesMaster> MeetingMinutesMasters { get; set; }
        public DbSet<MeetingMinutesDetails> MeetingMinutesDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CorporateCustomer>().ToTable("Corporate_Customer_Tbl");
            modelBuilder.Entity<IndividualCustomer>().ToTable("Individual_Customer_Tbl");
            modelBuilder.Entity<ProductService>().ToTable("Products_Service_Tbl");
            modelBuilder.Entity<MeetingMinutesMaster>().ToTable("Meeting_Minutes_Master_Tbl");
            modelBuilder.Entity<MeetingMinutesDetails>().ToTable("Meeting_Minutes_Details_Tbl");
        }
    }
}