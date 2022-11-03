using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOddPersistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSync = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SportId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    SpecialistId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Sport_SportId",
                        column: x => x.SportId,
                        principalTable: "Sport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BetType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    SpecialistId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    AwayTeam = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetType_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Odd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OddValue = table.Column<double>(type: "float", nullable: false),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    H2hId = table.Column<int>(type: "int", nullable: true),
                    IndividualResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odd_BetType_H2hId",
                        column: x => x.H2hId,
                        principalTable: "BetType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Odd_BetType_IndividualResultId",
                        column: x => x.IndividualResultId,
                        principalTable: "BetType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetType_GameId",
                table: "BetType",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_SportId",
                table: "Game",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Odd_H2hId",
                table: "Odd",
                column: "H2hId");

            migrationBuilder.CreateIndex(
                name: "IX_Odd_IndividualResultId",
                table: "Odd",
                column: "IndividualResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odd");

            migrationBuilder.DropTable(
                name: "BetType");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Sport");
        }
    }
}
