using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampSleepaway.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Councelor",
                columns: table => new
                {
                    CouncelorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoriteNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Councelor", x => x.CouncelorId);
                });

            migrationBuilder.CreateTable(
                name: "Cabin",
                columns: table => new
                {
                    CabinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouncelorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.CabinId);
                    table.ForeignKey(
                        name: "FK_Cabin_Councelor_CouncelorId",
                        column: x => x.CouncelorId,
                        principalTable: "Councelor",
                        principalColumn: "CouncelorId");
                });

            migrationBuilder.CreateTable(
                name: "Camper",
                columns: table => new
                {
                    CamperId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinId = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camper", x => x.CamperId);
                    table.ForeignKey(
                        name: "FK_Camper_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "CabinId");
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CamperId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visit_Camper_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Camper",
                        principalColumn: "CamperId");
                });

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    NextOfKinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitId = table.Column<int>(type: "int", nullable: true),
                    RelationType = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.NextOfKinId);
                    table.ForeignKey(
                        name: "FK_NextOfKin_Visit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visit",
                        principalColumn: "VisitId");
                });

            migrationBuilder.CreateTable(
                name: "CamperNextOfKins",
                columns: table => new
                {
                    CamperId = table.Column<int>(type: "int", nullable: false),
                    NextOfKinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamperNextOfKins", x => new { x.CamperId, x.NextOfKinId });
                    table.ForeignKey(
                        name: "FK_CamperNextOfKins_Camper_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Camper",
                        principalColumn: "CamperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CamperNextOfKins_NextOfKin_NextOfKinId",
                        column: x => x.NextOfKinId,
                        principalTable: "NextOfKin",
                        principalColumn: "NextOfKinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cabin_CouncelorId",
                table: "Cabin",
                column: "CouncelorId",
                unique: true,
                filter: "[CouncelorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Camper_CabinId",
                table: "Camper",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_CamperNextOfKins_NextOfKinId",
                table: "CamperNextOfKins",
                column: "NextOfKinId");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_VisitId",
                table: "NextOfKin",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_CamperId",
                table: "Visit",
                column: "CamperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CamperNextOfKins");

            migrationBuilder.DropTable(
                name: "NextOfKin");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Camper");

            migrationBuilder.DropTable(
                name: "Cabin");

            migrationBuilder.DropTable(
                name: "Councelor");
        }
    }
}
