//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Runtime.Remoting.Contexts;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAL.EF
//{
//    internal class UMSContext : DbContext
//    {
//        public DbSet<Person> Students { get; set; }

//    }
//}

using System.Data.Entity;

namespace DAL.EF
{
    internal class UMSContext : DbContext
    {
        // Define the DbSet for Persons
        public DbSet<Person> Persons { get; set; }

        // Optional: Configure the connection string
        public UMSContext() : base("name=UMSContext")
        {
        }
    }
}
