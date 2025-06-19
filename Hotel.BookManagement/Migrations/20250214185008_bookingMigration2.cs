using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.BookManagement.Migrations
{
    /// <inheritdoc />
    public partial class bookingMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "Books");
        }
    }
}
