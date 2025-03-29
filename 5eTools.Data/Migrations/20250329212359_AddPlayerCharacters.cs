using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerCharacters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Proficient",
                table: "Wisdom",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proficient",
                table: "Strength",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proficient",
                table: "Intelligence",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proficient",
                table: "Dexterity",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proficient",
                table: "Constitution",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Proficient",
                table: "Charisma",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowsMulticlassing",
                table: "Campaign",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CasterType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasterType", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExhaustionLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhaustionLevel", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrimalCompanionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    HitPointMultiplier = table.Column<int>(type: "int", nullable: false),
                    HitDieSize = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<string>(type: "longtext", nullable: false),
                    Size = table.Column<string>(type: "longtext", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Dexterity = table.Column<int>(type: "int", nullable: false),
                    Constitution = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Wisdom = table.Column<int>(type: "int", nullable: false),
                    Charisma = table.Column<int>(type: "int", nullable: false),
                    AbilityName = table.Column<string>(type: "longtext", nullable: false),
                    AbilityDescription = table.Column<string>(type: "longtext", nullable: false),
                    ActionName = table.Column<string>(type: "longtext", nullable: false),
                    ActionDescription = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimalCompanionType", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Resolve",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolve", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SpellSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstLevel = table.Column<int>(type: "int", nullable: false),
                    SecondLevel = table.Column<int>(type: "int", nullable: false),
                    ThirdLevel = table.Column<int>(type: "int", nullable: false),
                    FourthLevel = table.Column<int>(type: "int", nullable: false),
                    FifthLevel = table.Column<int>(type: "int", nullable: false),
                    SixthLevel = table.Column<int>(type: "int", nullable: false),
                    SeventhLevel = table.Column<int>(type: "int", nullable: false),
                    EigthLevel = table.Column<int>(type: "int", nullable: false),
                    NinthLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSlots", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StressLevel = table.Column<int>(type: "int", nullable: false),
                    StressThreshold = table.Column<int>(type: "int", nullable: false),
                    StressMaximum = table.Column<int>(type: "int", nullable: false),
                    MeditationDiceUsed = table.Column<int>(type: "int", nullable: false),
                    StressStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stress", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StressType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StressType", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsedSpellSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstLevel = table.Column<int>(type: "int", nullable: false),
                    SecondLevel = table.Column<int>(type: "int", nullable: false),
                    ThirdLevel = table.Column<int>(type: "int", nullable: false),
                    FourthLevel = table.Column<int>(type: "int", nullable: false),
                    FifthLevel = table.Column<int>(type: "int", nullable: false),
                    SixthLevel = table.Column<int>(type: "int", nullable: false),
                    SeventhLevel = table.Column<int>(type: "int", nullable: false),
                    EigthLevel = table.Column<int>(type: "int", nullable: false),
                    NinthLevel = table.Column<int>(type: "int", nullable: false),
                    Warlock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedSpellSlots", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WarlockSpellSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "longtext", nullable: false),
                    Slots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarlockSpellSlots", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    HitDieSize = table.Column<int>(type: "int", nullable: false),
                    AverageHitDieRoll = table.Column<int>(type: "int", nullable: false),
                    ClassAbilityScore = table.Column<string>(type: "longtext", nullable: false),
                    CasterTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_CasterType_CasterTypeId",
                        column: x => x.CasterTypeId,
                        principalTable: "CasterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrimalCompanion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    HitPointMaximum = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    MaxHitPointReduction = table.Column<int>(type: "int", nullable: false),
                    TemporaryHitPoints = table.Column<int>(type: "int", nullable: false),
                    DeathSaveFailures = table.Column<int>(type: "int", nullable: false),
                    DeathSaveSuccesses = table.Column<int>(type: "int", nullable: false),
                    HitDiceUsed = table.Column<int>(type: "int", nullable: false),
                    StrengthOverride = table.Column<int>(type: "int", nullable: true),
                    DexterityOverride = table.Column<int>(type: "int", nullable: true),
                    ConstitutionOverride = table.Column<int>(type: "int", nullable: true),
                    IntelligenceOverride = table.Column<int>(type: "int", nullable: true),
                    WisdomOverride = table.Column<int>(type: "int", nullable: true),
                    CharismaOverride = table.Column<int>(type: "int", nullable: true),
                    PrimalCompanionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimalCompanion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimalCompanion_PrimalCompanionType_PrimalCompanionTypeId",
                        column: x => x.PrimalCompanionTypeId,
                        principalTable: "PrimalCompanionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StressStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MinimumRoll = table.Column<int>(type: "int", nullable: false),
                    MaximumRoll = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    StressTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StressStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StressStatus_StressType_StressTypeId",
                        column: x => x.StressTypeId,
                        principalTable: "StressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlayerCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    BaseArmorClass = table.Column<int>(type: "int", nullable: false),
                    ArmorClassBonus = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    HitPointMaximum = table.Column<int>(type: "int", nullable: false),
                    TemporaryHitPoints = table.Column<int>(type: "int", nullable: false),
                    MaxHitPointReduction = table.Column<int>(type: "int", nullable: false),
                    DeathSaveFailures = table.Column<int>(type: "int", nullable: false),
                    DeathSaveSuccesses = table.Column<int>(type: "int", nullable: false),
                    ToughFeat = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsCombatant = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DwarvenToughness = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SpellcasterLevel = table.Column<int>(type: "int", nullable: false),
                    WarlockLevel = table.Column<int>(type: "int", nullable: false),
                    ExhaustionLevel = table.Column<int>(type: "int", nullable: false),
                    StrengthId = table.Column<int>(type: "int", nullable: false),
                    DexterityId = table.Column<int>(type: "int", nullable: false),
                    ConstitutionId = table.Column<int>(type: "int", nullable: false),
                    IntelligenceId = table.Column<int>(type: "int", nullable: false),
                    WisdomId = table.Column<int>(type: "int", nullable: false),
                    CharismaId = table.Column<int>(type: "int", nullable: false),
                    ResolveId = table.Column<int>(type: "int", nullable: true),
                    StressId = table.Column<int>(type: "int", nullable: true),
                    UsedSpellSlotsId = table.Column<int>(type: "int", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Charisma_CharismaId",
                        column: x => x.CharismaId,
                        principalTable: "Charisma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Constitution_ConstitutionId",
                        column: x => x.ConstitutionId,
                        principalTable: "Constitution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Dexterity_DexterityId",
                        column: x => x.DexterityId,
                        principalTable: "Dexterity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Intelligence_IntelligenceId",
                        column: x => x.IntelligenceId,
                        principalTable: "Intelligence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Resolve_ResolveId",
                        column: x => x.ResolveId,
                        principalTable: "Resolve",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Strength_StrengthId",
                        column: x => x.StrengthId,
                        principalTable: "Strength",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Stress_StressId",
                        column: x => x.StressId,
                        principalTable: "Stress",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_UsedSpellSlots_UsedSpellSlotsId",
                        column: x => x.UsedSpellSlotsId,
                        principalTable: "UsedSpellSlots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacter_Wisdom_WisdomId",
                        column: x => x.WisdomId,
                        principalTable: "Wisdom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subclass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    PrimalCompanion = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ThirdCaster = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subclass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subclass_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CampaignSubclass",
                columns: table => new
                {
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    SubclassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignSubclass", x => new { x.CampaignId, x.SubclassId });
                    table.ForeignKey(
                        name: "FK_CampaignSubclass_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignSubclass_Subclass_SubclassId",
                        column: x => x.SubclassId,
                        principalTable: "Subclass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CharacterClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(type: "int", nullable: false),
                    HitDiceUsed = table.Column<int>(type: "int", nullable: false),
                    BaseClass = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BaseClassSaveDc = table.Column<int>(type: "int", nullable: false),
                    SubclassId = table.Column<int>(type: "int", nullable: false),
                    PrimalCompanionId = table.Column<int>(type: "int", nullable: true),
                    PlayerCharacterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterClass_PlayerCharacter_PlayerCharacterId",
                        column: x => x.PlayerCharacterId,
                        principalTable: "PlayerCharacter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterClass_PrimalCompanion_PrimalCompanionId",
                        column: x => x.PrimalCompanionId,
                        principalTable: "PrimalCompanion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterClass_Subclass_SubclassId",
                        column: x => x.SubclassId,
                        principalTable: "Subclass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignSubclass_SubclassId",
                table: "CampaignSubclass",
                column: "SubclassId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClass_PlayerCharacterId",
                table: "CharacterClass",
                column: "PlayerCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClass_PrimalCompanionId",
                table: "CharacterClass",
                column: "PrimalCompanionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClass_SubclassId",
                table: "CharacterClass",
                column: "SubclassId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_CasterTypeId",
                table: "Class",
                column: "CasterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_CampaignId",
                table: "PlayerCharacter",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_CharismaId",
                table: "PlayerCharacter",
                column: "CharismaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_ConstitutionId",
                table: "PlayerCharacter",
                column: "ConstitutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_DexterityId",
                table: "PlayerCharacter",
                column: "DexterityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_IntelligenceId",
                table: "PlayerCharacter",
                column: "IntelligenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_ResolveId",
                table: "PlayerCharacter",
                column: "ResolveId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_StrengthId",
                table: "PlayerCharacter",
                column: "StrengthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_StressId",
                table: "PlayerCharacter",
                column: "StressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_UsedSpellSlotsId",
                table: "PlayerCharacter",
                column: "UsedSpellSlotsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_UserId",
                table: "PlayerCharacter",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacter_WisdomId",
                table: "PlayerCharacter",
                column: "WisdomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrimalCompanion_PrimalCompanionTypeId",
                table: "PrimalCompanion",
                column: "PrimalCompanionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StressStatus_StressTypeId",
                table: "StressStatus",
                column: "StressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subclass_ClassId",
                table: "Subclass",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignSubclass");

            migrationBuilder.DropTable(
                name: "CharacterClass");

            migrationBuilder.DropTable(
                name: "ExhaustionLevel");

            migrationBuilder.DropTable(
                name: "SpellSlots");

            migrationBuilder.DropTable(
                name: "StressStatus");

            migrationBuilder.DropTable(
                name: "WarlockSpellSlots");

            migrationBuilder.DropTable(
                name: "PlayerCharacter");

            migrationBuilder.DropTable(
                name: "PrimalCompanion");

            migrationBuilder.DropTable(
                name: "Subclass");

            migrationBuilder.DropTable(
                name: "StressType");

            migrationBuilder.DropTable(
                name: "Resolve");

            migrationBuilder.DropTable(
                name: "Stress");

            migrationBuilder.DropTable(
                name: "UsedSpellSlots");

            migrationBuilder.DropTable(
                name: "PrimalCompanionType");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "CasterType");

            migrationBuilder.DropColumn(
                name: "Proficient",
                table: "Wisdom");

            migrationBuilder.DropColumn(
                name: "Proficient",
                table: "Strength");

            migrationBuilder.DropColumn(
                name: "Proficient",
                table: "Intelligence");

            migrationBuilder.DropColumn(
                name: "Proficient",
                table: "Dexterity");

            migrationBuilder.DropColumn(
                name: "Proficient",
                table: "Constitution");

            migrationBuilder.DropColumn(
                name: "Proficient",
                table: "Charisma");

            migrationBuilder.DropColumn(
                name: "AllowsMulticlassing",
                table: "Campaign");
        }
    }
}
