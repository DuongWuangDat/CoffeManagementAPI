using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class dosmth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PayTypes_PayTypeId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Staff_StaffId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherTypes_VoucherTypeId",
                table: "Vouchers");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PayTypes_PayTypeId",
                table: "Bills",
                column: "PayTypeId",
                principalTable: "PayTypes",
                principalColumn: "PayTypeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Staff_StaffId",
                table: "Bills",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "VoucherID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId",
                principalTable: "CustomerTypes",
                principalColumn: "CustomerTypeID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherTypes_VoucherTypeId",
                table: "Vouchers",
                column: "VoucherTypeId",
                principalTable: "VoucherTypes",
                principalColumn: "VoucherTypeId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PayTypes_PayTypeId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Staff_StaffId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_VoucherTypes_VoucherTypeId",
                table: "Vouchers");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PayTypes_PayTypeId",
                table: "Bills",
                column: "PayTypeId",
                principalTable: "PayTypes",
                principalColumn: "PayTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Staff_StaffId",
                table: "Bills",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_VoucherId",
                table: "Bills",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "VoucherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId",
                principalTable: "CustomerTypes",
                principalColumn: "CustomerTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_VoucherTypes_VoucherTypeId",
                table: "Vouchers",
                column: "VoucherTypeId",
                principalTable: "VoucherTypes",
                principalColumn: "VoucherTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
