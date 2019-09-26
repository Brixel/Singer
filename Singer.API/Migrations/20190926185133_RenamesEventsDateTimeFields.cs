using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class RenamesEventsDateTimeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyEndTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DailyStartTime",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StartRegistrationDate",
                table: "Events",
                newName: "StartRegistrationDateTime");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Events",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "FinalCancellationDate",
                table: "Events",
                newName: "FinalCancellationDateTime");

            migrationBuilder.RenameColumn(
                name: "EndRegistrationDate",
                table: "Events",
                newName: "EndRegistrationDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Events",
                newName: "EndDateTime");

            migrationBuilder.RenameColumn(
                name: "DayCareBeforeStartTime",
                table: "Events",
                newName: "DayCareBeforeStartDateTime");

            migrationBuilder.RenameColumn(
                name: "DayCareAfterEndTime",
                table: "Events",
                newName: "DayCareAfterEndDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartRegistrationDateTime",
                table: "Events",
                newName: "StartRegistrationDate");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Events",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "FinalCancellationDateTime",
                table: "Events",
                newName: "FinalCancellationDate");

            migrationBuilder.RenameColumn(
                name: "EndRegistrationDateTime",
                table: "Events",
                newName: "EndRegistrationDate");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "Events",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "DayCareBeforeStartDateTime",
                table: "Events",
                newName: "DayCareBeforeStartTime");

            migrationBuilder.RenameColumn(
                name: "DayCareAfterEndDateTime",
                table: "Events",
                newName: "DayCareAfterEndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DailyEndTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DailyStartTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
