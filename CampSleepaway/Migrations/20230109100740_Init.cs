﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampSleepaway.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabin",
                columns: table => new
                {
                    CabinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.CabinId);
                });

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    NextOfKinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationType = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.NextOfKinId);
                });

            migrationBuilder.CreateTable(
                name: "Camper",
                columns: table => new
                {
                    CamperId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camper", x => x.CamperId);
                    table.ForeignKey(
                        name: "FK_Camper_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "CabinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Councelor",
                columns: table => new
                {
                    CouncelorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinId = table.Column<int>(type: "int", nullable: false),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Councelor", x => x.CouncelorId);
                    table.ForeignKey(
                        name: "FK_Councelor_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "CabinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CamperNextOfKin",
                columns: table => new
                {
                    NextOfKinId = table.Column<int>(type: "int", nullable: false),
                    CamperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamperNextOfKin", x => new { x.CamperId, x.NextOfKinId });
                    table.ForeignKey(
                        name: "FK_CamperNextOfKin_Camper_CamperId",
                        column: x => x.CamperId,
                        principalTable: "Camper",
                        principalColumn: "CamperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CamperNextOfKin_NextOfKin_NextOfKinId",
                        column: x => x.NextOfKinId,
                        principalTable: "NextOfKin",
                        principalColumn: "NextOfKinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camper_CabinId",
                table: "Camper",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_CamperNextOfKin_NextOfKinId",
                table: "CamperNextOfKin",
                column: "NextOfKinId");

            migrationBuilder.CreateIndex(
                name: "IX_Councelor_CabinId",
                table: "Councelor",
                column: "CabinId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CamperNextOfKin");

            migrationBuilder.DropTable(
                name: "Councelor");

            migrationBuilder.DropTable(
                name: "Camper");

            migrationBuilder.DropTable(
                name: "NextOfKin");

            migrationBuilder.DropTable(
                name: "Cabin");
        }
    }
}
