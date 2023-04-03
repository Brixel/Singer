using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class AddsEventsAndEventLocations : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EventLocations",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: true),
                Address = table.Column<string>(nullable: true),
                PostalCode = table.Column<string>(nullable: true),
                City = table.Column<string>(nullable: true),
                Country = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventLocations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Events",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Title = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                AllowedAgeGroups = table.Column<int>(nullable: false),
                LocationId = table.Column<Guid>(nullable: false),
                MaxRegistrants = table.Column<int>(nullable: false),
                Cost = table.Column<decimal>(nullable: false),
                StartDate = table.Column<DateTime>(nullable: false),
                DailyStartTime = table.Column<DateTime>(nullable: false),
                DailyEndTime = table.Column<DateTime>(nullable: false),
                LastCancellationDate = table.Column<DateTime>(nullable: false),
                FullTimeSpanRegRequired = table.Column<bool>(nullable: false),
                BeforeAndAfterCare = table.Column<bool>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Events", x => x.Id);
                table.ForeignKey(
                    name: "FK_Events_EventLocations_LocationId",
                    column: x => x.LocationId,
                    principalTable: "EventLocations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Events_LocationId",
            table: "Events",
            column: "LocationId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Events");

        migrationBuilder.DropTable(
            name: "EventLocations");
    }
}
