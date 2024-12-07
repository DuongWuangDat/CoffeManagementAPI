using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatetabletype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableTypeId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TableTypes",
                columns: table => new
                {
                    TableTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNameType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTypes", x => x.TableTypeID);
                });

            migrationBuilder.InsertData(
                table: "TableTypes",
                columns: new[] { "TableTypeID", "TableNameType" },
                values: new object[,]
                {
                    { 1, "1 person" },
                    { 2, "2 people" },
                    { 3, "4 people" },
                    { 4, "6 people" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableTypeId",
                table: "Tables",
                column: "TableTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables",
                column: "TableTypeId",
                principalTable: "TableTypes",
                principalColumn: "TableTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_TableTypes_TableTypeId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "TableTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tables_TableTypeId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "TableTypeId",
                table: "Tables");
        }
    }
}
