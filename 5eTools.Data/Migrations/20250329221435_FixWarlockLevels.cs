using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixWarlockLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "WarlockSpellSlots",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "WarlockSpellSlots",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
