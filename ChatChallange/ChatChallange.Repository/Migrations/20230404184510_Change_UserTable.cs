using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatChallange.Repository.Migrations
{
    public partial class Change_UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersChat");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "UsersChat",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "UsersChat");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersChat",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }
    }
}
