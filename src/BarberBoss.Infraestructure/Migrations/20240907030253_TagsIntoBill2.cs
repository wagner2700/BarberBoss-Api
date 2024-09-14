using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBoss.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class TagsIntoBill2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Fatura",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Fatura");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
