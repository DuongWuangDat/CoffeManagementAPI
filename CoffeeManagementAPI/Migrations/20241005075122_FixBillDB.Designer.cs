﻿// <auto-generated />
using System;
using CoffeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241005075122_FixBillDB")]
    partial class FixBillDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoffeeManagementAPI.Model.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PayTypeId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Chưa thanh toán");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VoucherId")
                        .HasColumnType("int");

                    b.HasKey("BillId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PayTypeId");

                    b.HasIndex("StaffId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Bills", t =>
                        {
                            t.HasCheckConstraint("CK_STATUS_BILL", "[Status] IN ('Đã thanh toán', 'Chưa thanh toán')");
                        });
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.BillDetail", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPriceDtail")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId", "BillId");

                    b.HasIndex("BillId");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryName = "Đồ ăn"
                        },
                        new
                        {
                            CategoryID = 2,
                            CategoryName = "Đồ uống"
                        });
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerTypeId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CustomerID");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.CustomerType", b =>
                {
                    b.Property<int>("CustomerTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerTypeID"));

                    b.Property<decimal>("BoundaryRevenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerTypeID");

                    b.HasIndex("BoundaryRevenue")
                        .IsUnique();

                    b.ToTable("CustomerTypes");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.PayType", b =>
                {
                    b.Property<int>("PayTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayTypeId"));

                    b.Property<string>("PayTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PayTypeId");

                    b.ToTable("PayTypes");

                    b.HasData(
                        new
                        {
                            PayTypeId = 1,
                            PayTypeName = "Online"
                        },
                        new
                        {
                            PayTypeId = 2,
                            PayTypeName = "Tiền mặt"
                        });
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSoldOut")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffId"));

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StaffId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"));

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("TokenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Voucher", b =>
                {
                    b.Property<int>("VoucherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoucherID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxApply")
                        .HasColumnType("int");

                    b.Property<string>("VoucherCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VoucherTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("VoucherValue")
                        .HasColumnType("decimal (18,2)");

                    b.HasKey("VoucherID");

                    b.HasIndex("VoucherTypeId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.VoucherType", b =>
                {
                    b.Property<int>("VoucherTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoucherTypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VoucherTypeId");

                    b.ToTable("VoucherTypes");

                    b.HasData(
                        new
                        {
                            VoucherTypeId = 1,
                            TypeName = "Theo phần trăm"
                        },
                        new
                        {
                            VoucherTypeId = 2,
                            TypeName = "Giảm trực tiếp"
                        });
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Bill", b =>
                {
                    b.HasOne("CoffeeManagementAPI.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagementAPI.Model.PayType", "PayType")
                        .WithMany()
                        .HasForeignKey("PayTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagementAPI.Model.Staff", "Staff")
                        .WithMany("Bills")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagementAPI.Model.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("PayType");

                    b.Navigation("Staff");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.BillDetail", b =>
                {
                    b.HasOne("CoffeeManagementAPI.Model.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeManagementAPI.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Customer", b =>
                {
                    b.HasOne("CoffeeManagementAPI.Model.CustomerType", "CustomerType")
                        .WithMany("Customers")
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerType");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Product", b =>
                {
                    b.HasOne("CoffeeManagementAPI.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Voucher", b =>
                {
                    b.HasOne("CoffeeManagementAPI.Model.VoucherType", "VoucherType")
                        .WithMany()
                        .HasForeignKey("VoucherTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VoucherType");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.CustomerType", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("CoffeeManagementAPI.Model.Staff", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
