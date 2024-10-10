using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateCusType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "CustomerTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "CustomerTypes");
        }
    }
}
