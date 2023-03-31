using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class AddsEventRegistrationLogging : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "EventRegistrationLogs",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                EventRegistrationId = table.Column<Guid>(nullable: false),
                EventRegistrationChanges = table.Column<int>(nullable: false),
                EmailSent = table.Column<bool>(nullable: false),
                CreationDateTimeUTC = table.Column<DateTime>(nullable: false),
                ExecutedByUserId = table.Column<Guid>(nullable: false),
                NewLocationId = table.Column<Guid>(nullable: true),
                PreviousLocationId = table.Column<Guid>(nullable: true),
                NewStatus = table.Column<int>(nullable: true),
                PreviousStatus = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventRegistrationLogs", x => x.Id);
                table.ForeignKey(
                    name: "FK_EventRegistrationLogs_EventRegistrations_EventRegistrationId",
                    column: x => x.EventRegistrationId,
                    principalTable: "EventRegistrations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EventRegistrationLogs_AspNetUsers_ExecutedByUserId",
                    column: x => x.ExecutedByUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrationLogs_EventRegistrationId",
            table: "EventRegistrationLogs",
            column: "EventRegistrationId");

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrationLogs_ExecutedByUserId",
            table: "EventRegistrationLogs",
            column: "ExecutedByUserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EventRegistrationLogs");

    }
}
