using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayGreetings.DataAccess.Migrations
{
    public partial class personaddphonenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Person");
        }
    }
}
