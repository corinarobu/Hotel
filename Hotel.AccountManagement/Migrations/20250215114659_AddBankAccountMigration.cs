using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.AccountManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAccountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsUsers_AspNetUsers_UserId",
                schema: "Identity",
                table: "AccountsUsers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "Identity",
                table: "AccountsUsers",
                newName: "IBAN");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "Identity",
                table: "AccountsUsers",
                newName: "BankName");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                schema: "Identity",
                table: "AccountsUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsUsers_AspNetUsers_UserId",
                schema: "Identity",
                table: "AccountsUsers",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsUsers_AspNetUsers_UserId",
                schema: "Identity",
                table: "AccountsUsers");

            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "Identity",
                table: "AccountsUsers");

            migrationBuilder.RenameColumn(
                name: "IBAN",
                schema: "Identity",
                table: "AccountsUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "BankName",
                schema: "Identity",
                table: "AccountsUsers",
                newName: "Password");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsUsers_AspNetUsers_UserId",
                schema: "Identity",
                table: "AccountsUsers",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
