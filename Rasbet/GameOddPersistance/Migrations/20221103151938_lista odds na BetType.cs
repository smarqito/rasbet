using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOddPersistance.Migrations
{
    public partial class listaoddsnaBetType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odd_BetType_H2hId",
                table: "Odd");

            migrationBuilder.DropForeignKey(
                name: "FK_Odd_BetType_IndividualResultId",
                table: "Odd");

            migrationBuilder.DropIndex(
                name: "IX_Odd_H2hId",
                table: "Odd");

            migrationBuilder.DropColumn(
                name: "H2hId",
                table: "Odd");

            migrationBuilder.RenameColumn(
                name: "IndividualResultId",
                table: "Odd",
                newName: "BetTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Odd_IndividualResultId",
                table: "Odd",
                newName: "IX_Odd_BetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odd_BetType_BetTypeId",
                table: "Odd",
                column: "BetTypeId",
                principalTable: "BetType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odd_BetType_BetTypeId",
                table: "Odd");

            migrationBuilder.RenameColumn(
                name: "BetTypeId",
                table: "Odd",
                newName: "IndividualResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Odd_BetTypeId",
                table: "Odd",
                newName: "IX_Odd_IndividualResultId");

            migrationBuilder.AddColumn<int>(
                name: "H2hId",
                table: "Odd",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Odd_H2hId",
                table: "Odd",
                column: "H2hId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odd_BetType_H2hId",
                table: "Odd",
                column: "H2hId",
                principalTable: "BetType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Odd_BetType_IndividualResultId",
                table: "Odd",
                column: "IndividualResultId",
                principalTable: "BetType",
                principalColumn: "Id");
        }
    }
}
