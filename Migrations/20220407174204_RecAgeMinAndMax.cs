using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoptry.Migrations
{
    public partial class RecAgeMinAndMax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RecAgeMax",
                table: "Product",
                type: "decimal(2,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RecAgeMin",
                table: "Product",
                type: "decimal(2,1)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecAgeMax",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "RecAgeMin",
                table: "Product");
        }
    }
}
