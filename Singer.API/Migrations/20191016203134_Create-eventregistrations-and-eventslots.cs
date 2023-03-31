using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class Createeventregistrationsandeventslots : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Since the previous events were not in the correct structure, we delete them now
        migrationBuilder.Sql("DELETE Events");

        migrationBuilder.DropColumn(
            name: "currentRegistrants",
            table: "Events");

        migrationBuilder.CreateTable(
            name: "EventSlots",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                EventId = table.Column<Guid>(nullable: false),
                StartDateTime = table.Column<DateTime>(nullable: false),
                EndDateTime = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventSlots", x => x.Id);
                table.ForeignKey(
                    name: "FK_EventSlots_Events_EventId",
                    column: x => x.EventId,
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "EventRegistrations",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                EventSlotId = table.Column<Guid>(nullable: false),
                CareUserId = table.Column<Guid>(nullable: false),
                Status = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventRegistrations", x => x.Id);
                table.ForeignKey(
                    name: "FK_EventRegistrations_CareUsers_CareUserId",
                    column: x => x.CareUserId,
                    principalTable: "CareUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_EventRegistrations_EventSlots_EventSlotId",
                    column: x => x.EventSlotId,
                    principalTable: "EventSlots",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrations_CareUserId",
            table: "EventRegistrations",
            column: "CareUserId");

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrations_EventSlotId",
            table: "EventRegistrations",
            column: "EventSlotId");

        migrationBuilder.CreateIndex(
            name: "IX_EventSlots_EventId",
            table: "EventSlots",
            column: "EventId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EventRegistrations");

        migrationBuilder.DropTable(
            name: "EventSlots");

        migrationBuilder.AddColumn<int>(
            name: "currentRegistrants",
            table: "Events",
            nullable: false,
            defaultValue: 0);
    }
}
