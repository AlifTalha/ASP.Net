using MeetingMinutesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingMinutesManagement.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure the database is created and migrations are applied
            context.Database.EnsureCreated();

            // Check if database already has data to avoid duplicate seeding
            if (context.CorporateCustomers.Any() ||
                context.IndividualCustomers.Any() ||
                context.ProductServices.Any())
            {
                return; // DB has been seeded already
            }

            // Seed corporate customers
            var corporateCustomers = new CorporateCustomer[]
            {
                new CorporateCustomer { CustomerName = "ABC Corporation" },
                new CorporateCustomer { CustomerName = "XYZ Ltd" },
                new CorporateCustomer { CustomerName = "Global Solutions Inc" },
                new CorporateCustomer { CustomerName = "Tech Innovators" },
                new CorporateCustomer { CustomerName = "Digital Enterprises" }
            };
            context.CorporateCustomers.AddRange(corporateCustomers);

            // Seed individual customers
            var individualCustomers = new IndividualCustomer[]
            {
                new IndividualCustomer { CustomerName = "John Smith" },
                new IndividualCustomer { CustomerName = "Sarah Johnson" },
                new IndividualCustomer { CustomerName = "Michael Brown" },
                new IndividualCustomer { CustomerName = "Emily Davis" },
                new IndividualCustomer { CustomerName = "Robert Wilson" }
            };
            context.IndividualCustomers.AddRange(individualCustomers);

            // Seed product/services
            var productServices = new ProductService[]
            {
                new ProductService { Name = "Web Development", Unit = "Project" },
                new ProductService { Name = "Mobile App Development", Unit = "App" },
                new ProductService { Name = "IT Consulting", Unit = "Hour" },
                new ProductService { Name = "Cloud Hosting", Unit = "Month" },
                new ProductService { Name = "System Maintenance", Unit = "Year" },
                new ProductService { Name = "UI/UX Design", Unit = "Project" },
                new ProductService { Name = "Database Administration", Unit = "Hour" }
            };
            context.ProductServices.AddRange(productServices);

            // Save all changes to the database
            context.SaveChanges();
        }
    }
}