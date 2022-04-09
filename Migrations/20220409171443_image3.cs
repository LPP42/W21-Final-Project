using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoptry.Migrations
{
    public partial class image3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstImage",
                table: "Product",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstImage",
                table: "Product");
        }
    }
}
