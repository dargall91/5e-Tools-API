using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerCombatantData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitiativeBonus",
                table: "PlayerCharacter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitiativeRoll",
                table: "PlayerCharacter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "PlayerCharacter",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitiativeBonus",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "InitiativeRoll",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "PlayerCharacter");
        }
    }
}
