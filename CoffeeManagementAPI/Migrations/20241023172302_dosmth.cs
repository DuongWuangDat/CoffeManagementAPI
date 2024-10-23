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
            migrationBuilder.DropCheckConstraint(
                name: "CK_STATUS_BILL",
                table: "Bills");

            migrationBuilder.AddCheckConstraint(
                name: "CK_STATUS_BILL",
                table: "Bills",
                sql: "Status IN ('Pending', 'Successful')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_STATUS_BILL",
                table: "Bills");

            migrationBuilder.AddCheckConstraint(
                name: "CK_STATUS_BILL",
                table: "Bills",
                sql: "[Status] IN ('Đang chờ phục vụ', 'Đã hoàn thành')");
        }
    }
}
