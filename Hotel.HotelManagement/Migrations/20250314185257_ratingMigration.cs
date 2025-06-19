using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.HotelManagement.Migrations
{
    /// <inheritdoc />
    public partial class ratingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaqRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FaqId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaqRating_FAQ_FaqId",
                        column: x => x.FaqId,
                        principalTable: "FAQ",
                        principalColumn: "Id_FAQ",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaqRating_FaqId",
                table: "FaqRating",
                column: "FaqId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaqRating");
        }
    }
}
