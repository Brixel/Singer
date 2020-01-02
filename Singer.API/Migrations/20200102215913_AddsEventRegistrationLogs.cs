using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class AddsEventRegistrationLogs : Migration
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
                    CreationDateTimeUTC = table.Column<DateTime>(nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrationLogs_EventRegistrationId",
                table: "EventRegistrationLogs",
                column: "EventRegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRegistrationLogs");
        }
    }
}
