using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolTripPlanner.Data.Migrations
{
    public partial class ChangedSchoolTripProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "SchoolTrips",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "SchoolTrips",
                newName: "EndDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "SchoolTrips",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "SchoolTrips",
                newName: "EndDate");
        }
    }
}
