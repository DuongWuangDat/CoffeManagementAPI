using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables");

            migrationBuilder.AlterColumn<string>(
                name: "TableNameType",
                table: "TableTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TableTypes_TableNameType",
                table: "TableTypes",
                column: "TableNameType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorNumber",
                table: "Floors",
                column: "FloorNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables",
                column: "TableTypeId",
                principalTable: "TableTypes",
                principalColumn: "TableTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_TableTypes_TableNameType",
                table: "TableTypes");

            migrationBuilder.DropIndex(
                name: "IX_Floors_FloorNumber",
                table: "Floors");

            migrationBuilder.AlterColumn<string>(
                name: "TableNameType",
                table: "TableTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables",
                column: "TableTypeId",
                principalTable: "TableTypes",
                principalColumn: "TableTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
