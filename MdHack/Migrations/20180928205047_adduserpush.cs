using Microsoft.EntityFrameworkCore.Migrations;

namespace MdHack.Migrations
{
    public partial class adduserpush : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PushToken",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PushTokenData",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PushTokenData",
                table: "Users");
        }
    }
}
