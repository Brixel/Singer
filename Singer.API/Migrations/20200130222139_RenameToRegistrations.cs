using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class RenameToRegistrations : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_EventRegistrationLogs_EventRegistrations_EventRegistrationId",
            table: "EventRegistrationLogs");

        migrationBuilder.DropForeignKey(
            name: "FK_EventRegistrations_CareUsers_CareUserId",
            table: "EventRegistrations");

        migrationBuilder.DropForeignKey(
            name: "FK_EventRegistrations_EventLocations_DaycareLocationId",
            table: "EventRegistrations");

        migrationBuilder.DropForeignKey(
            name: "FK_EventRegistrations_EventSlots_EventSlotId",
            table: "EventRegistrations");

        migrationBuilder.DropPrimaryKey(
            name: "PK_EventRegistrations",
            table: "EventRegistrations");

        migrationBuilder.RenameTable(
            name: "EventRegistrations",
            newName: "Registrations");

        migrationBuilder.RenameIndex(
            name: "IX_EventRegistrations_CareUserId_EventSlotId",
            table: "Registrations",
            newName: "IX_Registrations_CareUserId_EventSlotId");

        migrationBuilder.RenameIndex(
            name: "IX_EventRegistrations_EventSlotId",
            table: "Registrations",
            newName: "IX_Registrations_EventSlotId");

        migrationBuilder.RenameIndex(
            name: "IX_EventRegistrations_DaycareLocationId",
            table: "Registrations",
            newName: "IX_Registrations_DaycareLocationId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Registrations",
            table: "Registrations",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_EventRegistrationLogs_Registrations_EventRegistrationId",
            table: "EventRegistrationLogs",
            column: "EventRegistrationId",
            principalTable: "Registrations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Registrations_CareUsers_CareUserId",
            table: "Registrations",
            column: "CareUserId",
            principalTable: "CareUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Registrations_EventLocations_DaycareLocationId",
            table: "Registrations",
            column: "DaycareLocationId",
            principalTable: "EventLocations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Registrations_EventSlots_EventSlotId",
            table: "Registrations",
            column: "EventSlotId",
            principalTable: "EventSlots",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_EventRegistrationLogs_Registrations_EventRegistrationId",
            table: "EventRegistrationLogs");

        migrationBuilder.DropForeignKey(
            name: "FK_Registrations_CareUsers_CareUserId",
            table: "Registrations");

        migrationBuilder.DropForeignKey(
            name: "FK_Registrations_EventLocations_DaycareLocationId",
            table: "Registrations");

        migrationBuilder.DropForeignKey(
            name: "FK_Registrations_EventSlots_EventSlotId",
            table: "Registrations");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Registrations",
            table: "Registrations");

        migrationBuilder.RenameTable(
            name: "Registrations",
            newName: "EventRegistrations");

        migrationBuilder.RenameIndex(
            name: "IX_Registrations_CareUserId_EventSlotId",
            table: "EventRegistrations",
            newName: "IX_EventRegistrations_CareUserId_EventSlotId");

        migrationBuilder.RenameIndex(
            name: "IX_Registrations_EventSlotId",
            table: "EventRegistrations",
            newName: "IX_EventRegistrations_EventSlotId");

        migrationBuilder.RenameIndex(
            name: "IX_Registrations_DaycareLocationId",
            table: "EventRegistrations",
            newName: "IX_EventRegistrations_DaycareLocationId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_EventRegistrations",
            table: "EventRegistrations",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_EventRegistrationLogs_EventRegistrations_EventRegistrationId",
            table: "EventRegistrationLogs",
            column: "EventRegistrationId",
            principalTable: "EventRegistrations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_EventRegistrations_CareUsers_CareUserId",
            table: "EventRegistrations",
            column: "CareUserId",
            principalTable: "CareUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_EventRegistrations_EventLocations_DaycareLocationId",
            table: "EventRegistrations",
            column: "DaycareLocationId",
            principalTable: "EventLocations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_EventRegistrations_EventSlots_EventSlotId",
            table: "EventRegistrations",
            column: "EventSlotId",
            principalTable: "EventSlots",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
