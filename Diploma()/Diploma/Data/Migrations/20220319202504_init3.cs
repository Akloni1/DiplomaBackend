using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma.Data.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Boxers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompetitionsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionsName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CompetitionsAddress = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompetitionsID);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionsBoxers",
                columns: table => new
                {
                    CompetitionsID = table.Column<int>(nullable: false),
                    BoxerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionsBoxers", x => new { x.CompetitionsID, x.BoxerId });
                    table.ForeignKey(
                        name: "FK_CompetitionsBoxers_Boxers",
                        column: x => x.BoxerId,
                        principalTable: "Boxers",
                        principalColumn: "BoxerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionsBoxers_Competitions",
                        column: x => x.CompetitionsID,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionsClubs",
                columns: table => new
                {
                    CompetitionsID = table.Column<int>(nullable: false),
                    BoxingClubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionsClubs", x => new { x.CompetitionsID, x.BoxingClubId });
                    table.ForeignKey(
                        name: "FK_CompetitionsClubs_BoxingClubs",
                        column: x => x.BoxingClubId,
                        principalTable: "BoxingClubs",
                        principalColumn: "BoxingClubId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitionsClubs_Competitions",
                        column: x => x.CompetitionsID,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsBoxers_BoxerId",
                table: "CompetitionsBoxers",
                column: "BoxerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionsClubs_BoxingClubId",
                table: "CompetitionsClubs",
                column: "BoxingClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionsBoxers");

            migrationBuilder.DropTable(
                name: "CompetitionsClubs");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Boxers");
        }
    }
}
