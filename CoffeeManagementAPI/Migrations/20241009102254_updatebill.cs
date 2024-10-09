using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatebill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bills_BillId",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Products_ProductId",
                table: "BillDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails");

            migrationBuilder.AddColumn<int>(
                name: "VoucherTypeIndex",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "VoucherValue",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "BillDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BillDetailId",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "BillDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "BillDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails",
                column: "BillDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductId",
                table: "BillDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bills_BillId",
                table: "BillDetails",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Products_ProductId",
                table: "BillDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bills_BillId",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Products_ProductId",
                table: "BillDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_ProductId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "VoucherTypeIndex",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "VoucherValue",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillDetailId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "BillDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails",
                columns: new[] { "ProductId", "BillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bills_BillId",
                table: "BillDetails",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Products_ProductId",
                table: "BillDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
