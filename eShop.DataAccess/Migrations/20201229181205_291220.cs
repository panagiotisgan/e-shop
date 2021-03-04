using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShop.DataAccess.Migrations
{
    public partial class _291220 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
