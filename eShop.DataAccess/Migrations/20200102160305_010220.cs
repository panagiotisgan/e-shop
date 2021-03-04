using Microsoft.EntityFrameworkCore.Migrations;

namespace eShop.DataAccess.Migrations
{
    public partial class _010220 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Users_UserId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_UserId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Users",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CredentialId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "Users",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Users",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialId",
                table: "Users",
                column: "CredentialId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Credentials_CredentialId",
                table: "Users",
                column: "CredentialId",
                principalTable: "Credentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Credentials_CredentialId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CredentialId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CredentialId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StateName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Credentials",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Cities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_UserId",
                table: "Credentials",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Users_UserId",
                table: "Credentials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
