using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class placediscountfrompricetoevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "CosmeticPrices");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Events",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                table: "CosmeticPrices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
