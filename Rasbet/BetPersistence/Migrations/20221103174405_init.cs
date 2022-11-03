using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetPersistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    WonValue = table.Column<double>(type: "float", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OddMultiple = table.Column<double>(type: "float", nullable: true),
                    OddsFinished = table.Column<int>(type: "int", nullable: true),
                    OddsWon = table.Column<int>(type: "int", nullable: true),
                    SelectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Selections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OddId = table.Column<int>(type: "int", nullable: false),
                    Odd = table.Column<double>(type: "float", nullable: false),
                    BetTypeId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    BetMultipleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selections_Bets_BetMultipleId",
                        column: x => x.BetMultipleId,
                        principalTable: "Bets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_SelectionId",
                table: "Bets",
                column: "SelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_BetMultipleId",
                table: "Selections",
                column: "BetMultipleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Selections_SelectionId",
                table: "Bets",
                column: "SelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Selections_SelectionId",
                table: "Bets");

            migrationBuilder.DropTable(
                name: "Selections");

            migrationBuilder.DropTable(
                name: "Bets");
        }
    }
}
