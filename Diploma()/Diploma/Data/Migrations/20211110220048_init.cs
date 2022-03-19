using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxingClubs",
                columns: table => new
                {
                    BoxingClubId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ClubAddress = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ClubRating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxingClubs", x => x.BoxingClubId);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    CoachID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SportsTitle = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BoxingClubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.CoachID);
                    table.ForeignKey(
                        name: "FK_Coaches_BoxingClubs",
                        column: x => x.BoxingClubId,
                        principalTable: "BoxingClubs",
                        principalColumn: "BoxingClubId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesClub",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Post = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BoxingClubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesClub", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeesClub_BoxingClubs",
                        column: x => x.BoxingClubId,
                        principalTable: "BoxingClubs",
                        principalColumn: "BoxingClubId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boxers",
                columns: table => new
                {
                    BoxerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
                    TrainingExperience = table.Column<double>(nullable: true),
                    NumberOfFightsHeld = table.Column<int>(nullable: true),
                    NumberOfWins = table.Column<int>(nullable: true),
                    Discharge = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CoachID = table.Column<int>(nullable: true),
                    BoxingClubId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxers", x => x.BoxerId);
                    table.ForeignKey(
                        name: "FK_Boxers_BoxingClubs",
                        column: x => x.BoxingClubId,
                        principalTable: "BoxingClubs",
                        principalColumn: "BoxingClubId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boxers_Coaches",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxers_BoxingClubId",
                table: "Boxers",
                column: "BoxingClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxers_CoachID",
                table: "Boxers",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_BoxingClubId",
                table: "Coaches",
                column: "BoxingClubId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesClub_BoxingClubId",
                table: "EmployeesClub",
                column: "BoxingClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxers");

            migrationBuilder.DropTable(
                name: "EmployeesClub");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "BoxingClubs");
        }
    }
}
