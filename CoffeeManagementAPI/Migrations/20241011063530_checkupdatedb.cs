using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class checkupdatedb : Migration
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
                sql: "[Status] IN ('Đang chờ phục vụ', 'Đã hoàn thành')");
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
                sql: "[Status] IN ('Đã thanh toán', 'Chưa thanh toán')");
        }
    }
}
