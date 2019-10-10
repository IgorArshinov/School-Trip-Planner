using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolTripPlanner.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Toddlers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<long>(nullable: false),
                    QRCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toddlers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Toddlers_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTrips",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolTrips_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolTripId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scans_SchoolTrips_SchoolTripId",
                        column: x => x.SchoolTripId,
                        principalTable: "SchoolTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTripToddlers",
                columns: table => new
                {
                    ToddlerId = table.Column<long>(nullable: false),
                    SchoolTripId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTripToddlers", x => new { x.SchoolTripId, x.ToddlerId });
                    table.ForeignKey(
                        name: "FK_SchoolTripToddlers_SchoolTrips_SchoolTripId",
                        column: x => x.SchoolTripId,
                        principalTable: "SchoolTrips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolTripToddlers_Toddlers_ToddlerId",
                        column: x => x.ToddlerId,
                        principalTable: "Toddlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScanToddlers",
                columns: table => new
                {
                    ToddlerId = table.Column<long>(nullable: false),
                    ScanId = table.Column<long>(nullable: false),
                    ToddlerIsScanned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanToddlers", x => new { x.ToddlerId, x.ScanId });
                    table.ForeignKey(
                        name: "FK_ScanToddlers_Scans_ScanId",
                        column: x => x.ScanId,
                        principalTable: "Scans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScanToddlers_Toddlers_ToddlerId",
                        column: x => x.ToddlerId,
                        principalTable: "Toddlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scans_SchoolTripId",
                table: "Scans",
                column: "SchoolTripId");

            migrationBuilder.CreateIndex(
                name: "IX_ScanToddlers_ScanId",
                table: "ScanToddlers",
                column: "ScanId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTrips_TeacherId",
                table: "SchoolTrips",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripToddlers_ToddlerId",
                table: "SchoolTripToddlers",
                column: "ToddlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Toddlers_ClassId",
                table: "Toddlers",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScanToddlers");

            migrationBuilder.DropTable(
                name: "SchoolTripToddlers");

            migrationBuilder.DropTable(
                name: "Scans");

            migrationBuilder.DropTable(
                name: "Toddlers");

            migrationBuilder.DropTable(
                name: "SchoolTrips");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
