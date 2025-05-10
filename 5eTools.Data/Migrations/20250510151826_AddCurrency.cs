using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //make new relationship initially nullable so it doesn't error
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PlayerCharacter",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Copper = table.Column<int>(type: "int", nullable: false),
                    Silver = table.Column<int>(type: "int", nullable: false),
                    Electrum = table.Column<int>(type: "int", nullable: false),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Platinum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            //for each character, create a currency, then populate the relationship
            migrationBuilder.Sql("INSERT INTO Currency (Id, Copper, Silver, Electrum, Gold, Platinum) SELECT Id, 0, 0, 0, 0, 0 FROM PlayerCharacter;");
            migrationBuilder.Sql("UPDATE PlayerCharacter SET CurrencyId = Id;");

            //make the relationship required
            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PlayerCharacter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stress_StressStatusId",
                table: "Stress",
                column: "StressStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_CurrencyId",
                table: "PlayerCharacter",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacter_Currency_CurrencyId",
                table: "PlayerCharacter",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stress_StressStatus_StressStatusId",
                table: "Stress",
                column: "StressStatusId",
                principalTable: "StressStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacter_Currency_CurrencyId",
                table: "PlayerCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_Stress_StressStatus_StressStatusId",
                table: "Stress");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Stress_StressStatusId",
                table: "Stress");

            migrationBuilder.DropIndex(
                name: "IX_PlayerCharacter_CurrencyId",
                table: "PlayerCharacter");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PlayerCharacter");
        }
    }
}
