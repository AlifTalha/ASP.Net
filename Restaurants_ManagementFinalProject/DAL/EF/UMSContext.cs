using System.Data.Entity;

namespace DAL.EF
{
    internal class UMSContext : DbContext
    {
        public UMSContext() : base("Server=DESKTOP-1DUMAFQ\\SQLEXPRESS;Database=TRP_Tabla;Trusted_Connection=True;")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Table)
                .WithMany()
                .HasForeignKey(o => o.TableId)
                .WillCascadeOnDelete(false);
        }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }


    }
}