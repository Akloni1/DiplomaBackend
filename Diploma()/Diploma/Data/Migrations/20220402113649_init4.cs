using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma.Data.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Coaches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Coaches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Coaches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Boxers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Boxers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Boxers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Boxers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Boxers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Boxers");
        }
    }
}
