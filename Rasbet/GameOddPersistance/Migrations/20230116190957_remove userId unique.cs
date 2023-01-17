using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOddPersistance.Migrations
{
    public partial class removeuserIdunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Follower_UserId",
                table: "Follower");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Follower",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Follower",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserId",
                table: "Follower",
                column: "UserId",
                unique: true);
        }
    }
}
