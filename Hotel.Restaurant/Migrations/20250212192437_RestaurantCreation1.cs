using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Restaurant.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantCreation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Meal_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meal_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Meal_Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Meal_Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Product = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type_Of_Product = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description_Product = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Meal_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id_Product);
                    table.ForeignKey(
                        name: "FK_Products_Meal_Meal_Id",
                        column: x => x.Meal_Id,
                        principalTable: "Meal",
                        principalColumn: "Meal_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Meal_Id",
                table: "Products",
                column: "Meal_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Meal");
        }
    }
}
