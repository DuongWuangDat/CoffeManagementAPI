using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_AppUserId1",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AppUserId1",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AppUserId",
                table: "Bills",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_AppUserId",
                table: "Bills",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_AppUserId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AppUserId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AppUserId1",
                table: "Bills",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_AppUserId1",
                table: "Bills",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
