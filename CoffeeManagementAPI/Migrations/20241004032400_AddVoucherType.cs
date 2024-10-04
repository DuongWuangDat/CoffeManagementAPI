using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VoucherTypes",
                columns: new[] { "VoucherTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Theo phần trăm" },
                    { 2, "Giảm trực tiếp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VoucherTypes",
                keyColumn: "VoucherTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VoucherTypes",
                keyColumn: "VoucherTypeId",
                keyValue: 2);
        }
    }
}
