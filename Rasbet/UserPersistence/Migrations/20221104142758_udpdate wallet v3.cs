using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserPersistence.Migrations
{
    public partial class udpdatewalletv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_WalletUserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WalletUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletUserId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallet_WalletId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wallet",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "WalletUserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletUserId",
                table: "Transactions",
                column: "WalletUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletUserId",
                table: "AspNetUsers",
                column: "WalletUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletUserId",
                table: "AspNetUsers",
                column: "WalletUserId",
                principalTable: "Wallet",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallet_WalletUserId",
                table: "Transactions",
                column: "WalletUserId",
                principalTable: "Wallet",
                principalColumn: "UserId");
        }
    }
}
