using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPayType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PayTypes",
                columns: new[] { "PayTypeId", "PayTypeName" },
                values: new object[,]
                {
                    { 1, "Online" },
                    { 2, "Tiền mặt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PayTypes",
                keyColumn: "PayTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PayTypes",
                keyColumn: "PayTypeId",
                keyValue: 2);
        }
    }
}
