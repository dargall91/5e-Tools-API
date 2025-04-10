using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5eTools.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCampaignOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Campaign",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_UserId",
                table: "Campaign",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_User_UserId",
                table: "Campaign",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_User_UserId",
                table: "Campaign");

            migrationBuilder.DropIndex(
                name: "IX_Campaign_UserId",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Campaign");
        }
    }
}
