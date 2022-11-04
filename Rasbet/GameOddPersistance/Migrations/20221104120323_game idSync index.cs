using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOddPersistance.Migrations
{
    public partial class gameidSyncindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetType_Game_GameId",
                table: "BetType");

            migrationBuilder.AlterColumn<string>(
                name: "IdSync",
                table: "Game",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "BetType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_IdSync",
                table: "Game",
                column: "IdSync");

            migrationBuilder.AddForeignKey(
                name: "FK_BetType_Game_GameId",
                table: "BetType",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetType_Game_GameId",
                table: "BetType");

            migrationBuilder.DropIndex(
                name: "IX_Game_IdSync",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "IdSync",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "BetType",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BetType_Game_GameId",
                table: "BetType",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id");
        }
    }
}
