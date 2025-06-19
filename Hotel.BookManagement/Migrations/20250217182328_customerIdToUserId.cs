using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.BookManagement.Migrations
{
    /// <inheritdoc />
    public partial class customerIdToUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Books",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Books",
                newName: "CustomerId");
        }
    }
}
