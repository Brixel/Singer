using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class RemovesSuperfluousDaycareFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DayCareAfterStartTime",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "DayCareBeforeEndTime",
            table: "Events");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
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
    }
}
