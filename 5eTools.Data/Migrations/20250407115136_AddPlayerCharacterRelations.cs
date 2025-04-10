using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerCharacterRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExhaustionLevel",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "SpellcasterLevel",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "WarlockLevel",
                table: "PlayerCharacter");

            migrationBuilder.AddColumn<int>(
                name: "ProficiencyBonusId",
                table: "PlayerCharacter",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "ExhaustionLevelId",
                table: "PlayerCharacter",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpellSlotsId",
                table: "PlayerCharacter",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarlockSpellSlotsId",
                table: "PlayerCharacter",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_ExhaustionLevelId",
                table: "PlayerCharacter",
                column: "ExhaustionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_ProficiencyBonusId",
                table: "PlayerCharacter",
                column: "ProficiencyBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_SpellSlotsId",
                table: "PlayerCharacter",
                column: "SpellSlotsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_WarlockSpellSlotsId",
                table: "PlayerCharacter",
                column: "WarlockSpellSlotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacter_ExhaustionLevel_ExhaustionLevelId",
                table: "PlayerCharacter",
                column: "ExhaustionLevelId",
                principalTable: "ExhaustionLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacter_ProficiencyBonus_ProficiencyBonusId",
                table: "PlayerCharacter",
                column: "ProficiencyBonusId",
                principalTable: "ProficiencyBonus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacter_SpellSlots_SpellSlotsId",
                table: "PlayerCharacter",
                column: "SpellSlotsId",
                principalTable: "SpellSlots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacter_WarlockSpellSlots_WarlockSpellSlotsId",
                table: "PlayerCharacter",
                column: "WarlockSpellSlotsId",
                principalTable: "WarlockSpellSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacter_ExhaustionLevel_ExhaustionLevelId",
                table: "PlayerCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacter_ProficiencyBonus_ProficiencyBonusId",
                table: "PlayerCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacter_SpellSlots_SpellSlotsId",
                table: "PlayerCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacter_WarlockSpellSlots_WarlockSpellSlotsId",
                table: "PlayerCharacter");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacter_ExhaustionLevelId",
                table: "PlayerCharacter");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacter_ProficiencyBonusId",
                table: "PlayerCharacter");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacter_SpellSlotsId",
                table: "PlayerCharacter");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacter_WarlockSpellSlotsId",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "ExhaustionLevelId",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "SpellSlotsId",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "WarlockSpellSlotsId",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "ProficiencyBonusId",
                table: "PlayerCharacter");

            migrationBuilder.AddColumn<int>(
                name: "WarlockLevel",
                table: "PlayerCharacter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExhaustionLevel",
                table: "PlayerCharacter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpellcasterLevel",
                table: "PlayerCharacter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
