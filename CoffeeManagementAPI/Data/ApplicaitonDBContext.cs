using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Data
{
    public class ApplicaitonDBContext :DbContext
    {
        public ApplicaitonDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
              
        }

        public DbSet<CustomerType> CustomerTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerType>().HasIndex(p => p.BoundaryRevenue).IsUnique();
        }
    }
}
