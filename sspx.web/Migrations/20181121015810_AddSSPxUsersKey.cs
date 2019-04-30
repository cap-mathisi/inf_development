using Microsoft.EntityFrameworkCore.Migrations;

namespace sspx.Migrations
{
    public partial class AddSSPxUsersKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SSPxUserKey",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSPxUserKey",
                table: "AspNetUsers");
        }
    }
}
