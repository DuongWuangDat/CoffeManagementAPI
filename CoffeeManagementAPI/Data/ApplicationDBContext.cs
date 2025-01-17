using CoffeeManagementAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
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

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<BookingTable> BookingTables { get; set; }

        public DbSet<TableType> TableTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerType>().HasIndex(p => p.BoundaryRevenue).IsUnique();
            modelBuilder.Entity<Staff>().HasIndex(p => p.Username).IsUnique();
            modelBuilder.Entity<Bill>().Property(p => p.Status).HasDefaultValue("Chưa thanh toán");
            modelBuilder.Entity<Bill>().ToTable(t => t.HasCheckConstraint("CK_STATUS_BILL", "Status IN ('Pending', 'Successful')"));
            modelBuilder.Entity<Voucher>().ToTable(t => t.HasCheckConstraint("CK_VOUCHER_DATE", "[CreatedDate] < [ExpiredDate]"));
            modelBuilder.Entity<Table>().ToTable(t => t.HasCheckConstraint("CK_TABLE_STATUS", "Status IN ('Booked', 'Not booked', 'Under repair')"));
            modelBuilder.Entity<Table>().HasIndex(p => p.TableNumber).IsUnique();
            modelBuilder.Entity<Floor>().HasIndex(p=> p.FloorNumber).IsUnique();
            modelBuilder.Entity<TableType>().HasIndex(p=>p.TableNameType).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.AverageStar).HasDefaultValue(0);


            //Set on delete

            List<Type> restrictEntity = new List<Type>
            {
                typeof(Bill)
            };

            List<Type> casadeEntity = new List<Type> { typeof(Table), typeof(Floor), typeof(TableType) };

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var foreginKeys = entityType.GetForeignKeys();

                foreach (var foreginKey in foreginKeys)
                {

                    if (restrictEntity.Contains(foreginKey.PrincipalEntityType.ClrType))
                    {
                        foreginKey.DeleteBehavior = DeleteBehavior.Restrict;
                    }
                    else if (casadeEntity.Contains(foreginKey.PrincipalEntityType.ClrType))
                    {
                        foreginKey.DeleteBehavior = DeleteBehavior.Cascade;
                    }
                    else
                    {
                        foreginKey.DeleteBehavior = DeleteBehavior.SetNull;
                    }

                }
            }




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
                    TypeName = "Percentage of bill"
                },
                new VoucherType
                {
                    VoucherTypeId=2,
                    TypeName = "Discount directly on invoice"
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

            List<TableType> tableTypes = new List<TableType>
            {
                new()
                {
                    TableTypeID = 1,
                    TableNameType = "1 person"
                },
                new()
                {
                    TableTypeID=2,
                    TableNameType= "2 people"
                },
                new()
                {
                    TableTypeID=3,
                    TableNameType= "4 people"
                },
                new()
                {
                    TableTypeID=4,
                    TableNameType= "6 people"
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<VoucherType>().HasData(voucherTypes);
            modelBuilder.Entity<PayType>().HasData(payTypes);
            modelBuilder.Entity<TableType>().HasData(tableTypes);


            //Ignore
           // modelBuilder.Entity<Floor>().Ignore(f=> f.Tables);
            modelBuilder.Entity<TableType>().Ignore(f=> f.Tables);
        }
    }
}
