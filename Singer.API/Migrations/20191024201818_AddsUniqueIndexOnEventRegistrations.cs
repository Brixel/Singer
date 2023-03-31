using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class AddsUniqueIndexOnEventRegistrations : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_EventRegistrations_CareUserId",
            table: "EventRegistrations");

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrations_CareUserId_EventSlotId",
            table: "EventRegistrations",
            columns: new[] { "CareUserId", "EventSlotId" },
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_EventRegistrations_CareUserId_EventSlotId",
            table: "EventRegistrations");

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrations_CareUserId",
            table: "EventRegistrations",
            column: "CareUserId");
    }
}
