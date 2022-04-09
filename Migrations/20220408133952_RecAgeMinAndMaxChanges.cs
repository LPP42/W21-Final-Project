using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shoptry.Migrations
{
    public partial class RecAgeMinAndMaxChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RecAgeMin",
                table: "Product",
                type: "decimal(3,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RecAgeMax",
                table: "Product",
                type: "decimal(3,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RecAgeMin",
                table: "Product",
                type: "decimal(2,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RecAgeMax",
                table: "Product",
                type: "decimal(2,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)",
                oldNullable: true);
        }
    }
}
