using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class AddsMissingFieldsForEvents : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "LastCancellationDate",
            table: "Events",
            newName: "StartRegistrationDate");

        migrationBuilder.RenameColumn(
            name: "FullTimeSpanRegRequired",
            table: "Events",
            newName: "RegistrationOnDailyBasis");

        migrationBuilder.RenameColumn(
            name: "BeforeAndAfterCare",
            table: "Events",
            newName: "HasDayCareBefore");

        migrationBuilder.AddColumn<DateTime>(
            name: "DayCareAfterEndTime",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DayCareAfterStartTime",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DayCareBeforeEndTime",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DayCareBeforeStartTime",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "EndDate",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "EndRegistrationDate",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "FinalCancellationDate",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "HasDayCareAfter",
            table: "Events",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<int>(
            name: "currentRegistrants",
            table: "Events",
            nullable: false,
            defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DayCareAfterEndTime",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "DayCareAfterStartTime",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "DayCareBeforeEndTime",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "DayCareBeforeStartTime",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "EndDate",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "EndRegistrationDate",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "FinalCancellationDate",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "HasDayCareAfter",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "currentRegistrants",
            table: "Events");

        migrationBuilder.RenameColumn(
            name: "StartRegistrationDate",
            table: "Events",
            newName: "LastCancellationDate");

        migrationBuilder.RenameColumn(
            name: "RegistrationOnDailyBasis",
            table: "Events",
            newName: "FullTimeSpanRegRequired");

        migrationBuilder.RenameColumn(
            name: "HasDayCareBefore",
            table: "Events",
            newName: "BeforeAndAfterCare");
    }
}
