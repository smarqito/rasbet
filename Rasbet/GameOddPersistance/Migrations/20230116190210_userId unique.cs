using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOddPersistance.Migrations
{
    public partial class userIdunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follower_Game_GameId",
                table: "Follower");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Follower",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Follower",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserId",
                table: "Follower",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Follower_Game_GameId",
                table: "Follower",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follower_Game_GameId",
                table: "Follower");

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

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Follower",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Follower_Game_GameId",
                table: "Follower",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id");
        }
    }
}
