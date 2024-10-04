using CoffeeManagementAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Data
{
    public class ApplicaitonDBContext : DbContext
    {
        public ApplicaitonDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
              
        }

        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<PayType> PayTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Token> Tokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerType>().HasIndex(p => p.BoundaryRevenue).IsUnique();
            modelBuilder.Entity<Staff>().HasIndex(p=> p.Username).IsUnique();
            modelBuilder.Entity<Bill>().Property(p => p.Status).HasDefaultValue("Chưa thanh toán");
            modelBuilder.Entity<Bill>().ToTable(t=> t.HasCheckConstraint("CK_STATUS_BILL", "[Status] IN ('Đã thanh toán', 'Chưa thanh toán')"));
            modelBuilder.Entity<BillDetail>().HasKey(e => new {e.ProductId, e.BillId});

            List<Category> categories = new List<Category>
            {
                new Category
                {
                    CategoryID=1,
                    CategoryName = "Đồ ăn",
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Đồ uống"
                }
            };

            List<VoucherType> voucherTypes = new List<VoucherType>
            {
                new VoucherType
                {
                    VoucherTypeId = 1,
                    TypeName = "Theo phần trăm"
                },
                new VoucherType
                {
                    VoucherTypeId=2,
                    TypeName = "Giảm trực tiếp"
                }
            };

            List<PayType> payTypes = new List<PayType>
            {
                new()
                {
                    PayTypeId = 1,
                    PayTypeName = "Online"
                },
                new()
                {
                    PayTypeId =2 ,
                    PayTypeName = "Tiền mặt"
                }
            };


            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<VoucherType>().HasData(voucherTypes);
            modelBuilder.Entity<PayType>().HasData(payTypes);

        }
    }
}
