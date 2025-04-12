using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStressRolls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumRoll",
                table: "StressStatus");

            migrationBuilder.DropColumn(
                name: "MinimumRoll",
                table: "StressStatus");

            migrationBuilder.AddColumn<int>(
                name: "Roll",
                table: "StressStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumRoll",
                table: "StressType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumRoll",
                table: "StressType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumRoll",
                table: "StressType");

            migrationBuilder.DropColumn(
                name: "MinimumRoll",
                table: "StressType");

            migrationBuilder.DropColumn(
                name: "Roll",
                table: "StressStatus");

            migrationBuilder.AddColumn<int>(
                name: "MinimumRoll",
                table: "StressStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumRoll",
                table: "StressStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
