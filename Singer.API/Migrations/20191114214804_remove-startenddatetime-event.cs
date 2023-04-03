using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class removestartenddatetimeevent : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "EndDateTime",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "StartDateTime",
            table: "Events");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "EndDateTime",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "StartDateTime",
            table: "Events",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }
}
