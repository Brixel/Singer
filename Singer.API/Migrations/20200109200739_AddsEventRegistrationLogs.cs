using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class AddsEventRegistrationLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventRegistrationLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventRegistrationId = table.Column<Guid>(nullable: false),
                    EventRegistrationChanges = table.Column<int>(nullable: false),
                    EmailSent = table.Column<bool>(nullable: false),
                    CreationDateTimeUTC = table.Column<DateTime>(nullable: false),
                    NewLocationIdId = table.Column<Guid>(nullable: true),
                    PreviousLocationId = table.Column<Guid>(nullable: true),
                    NewStatus = table.Column<int>(nullable: true),
                    PreviousStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrationLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRegistrationLog_EventRegistrations_EventRegistrationId",
                        column: x => x.EventRegistrationId,
                        principalTable: "EventRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrationLog_EventRegistrationId",
                table: "EventRegistrationLog",
                column: "EventRegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRegistrationLog");
        }
    }
}
