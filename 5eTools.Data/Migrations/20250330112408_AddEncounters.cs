using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncounterXpThreshold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Easy = table.Column<int>(type: "int", nullable: false),
                    Medium = table.Column<int>(type: "int", nullable: false),
                    Hard = table.Column<int>(type: "int", nullable: false),
                    Deadly = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncounterXpThreshold", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Encounter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    HasLairAction = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsArchived = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MusicId = table.Column<int>(type: "int", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encounter_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encounter_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EncounterMonster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InitiativeRoll = table.Column<int>(type: "int", nullable: false),
                    IsInvisible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsReinforcement = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMinion = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: false),
                    MonsterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncounterMonster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncounterMonster_Encounter_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncounterMonster_Monster_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Encounter_CampaignId",
                table: "Encounter",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounter_MusicId",
                table: "Encounter",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_EncounterMonster_EncounterId",
                table: "EncounterMonster",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_EncounterMonster_MonsterId",
                table: "EncounterMonster",
                column: "MonsterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncounterMonster");

            migrationBuilder.DropTable(
                name: "EncounterXpThreshold");

            migrationBuilder.DropTable(
                name: "Encounter");

            migrationBuilder.DropTable(
                name: "Music");
        }
    }
}
