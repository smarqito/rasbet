using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserPersistence.Migrations
{
    public partial class tiraraccepteddosupdateInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Updates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Updates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
