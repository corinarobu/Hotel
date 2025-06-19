using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.HotelManagement.Migrations
{
    /// <inheritdoc />
    public partial class removeMarkFromFAQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaqRating_FAQ_FaqId",
                table: "FaqRating");

            migrationBuilder.DropIndex(
                name: "IX_FaqRating_FaqId",
                table: "FaqRating");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "FAQ");

            migrationBuilder.RenameColumn(
                name: "FaqId",
                table: "FaqRating",
                newName: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "FaqRating",
                newName: "FaqId");

            migrationBuilder.AddColumn<decimal>(
                name: "Mark",
                table: "FAQ",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_FaqRating_FaqId",
                table: "FaqRating",
                column: "FaqId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaqRating_FAQ_FaqId",
                table: "FaqRating",
                column: "FaqId",
                principalTable: "FAQ",
                principalColumn: "Id_FAQ",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
