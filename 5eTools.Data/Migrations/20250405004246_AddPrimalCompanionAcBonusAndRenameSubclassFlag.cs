using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimalCompanionAcBonusAndRenameSubclassFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimalCompanion",
                table: "Subclass",
                newName: "HasPrimalCompanion");

            migrationBuilder.AddColumn<int>(
                name: "ArmorClassBonus",
                table: "PrimalCompanion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorClassBonus",
                table: "PrimalCompanion");

            migrationBuilder.RenameColumn(
                name: "HasPrimalCompanion",
                table: "Subclass",
                newName: "PrimalCompanion");
        }
    }
}
