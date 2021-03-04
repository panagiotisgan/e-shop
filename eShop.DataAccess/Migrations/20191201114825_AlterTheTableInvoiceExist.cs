using Microsoft.EntityFrameworkCore.Migrations;

namespace eShop.DataAccess.Migrations
{
    public partial class AlterTheTableInvoiceExist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Invoice",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "Invoice",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
