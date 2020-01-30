using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class ImproveRegistrationsForDayAndNightCare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_EventSlots_EventSlotId",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_CareUserId_EventSlotId",
                table: "EventRegistrations");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventSlotId",
                table: "EventRegistrations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "EventRegistrations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EventRegistrationType",
                table: "EventRegistrations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTime",
                table: "EventRegistrations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_CareUserId_EventSlotId",
                table: "EventRegistrations",
                columns: new[] { "CareUserId", "EventSlotId" },
                unique: true,
                filter: "[EventSlotId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_EventSlots_EventSlotId",
                table: "EventRegistrations",
                column: "EventSlotId",
                principalTable: "EventSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_EventSlots_EventSlotId",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_CareUserId_EventSlotId",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "EventRegistrationType",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "StartDateTime",
                table: "EventRegistrations");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventSlotId",
                table: "EventRegistrations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_CareUserId_EventSlotId",
                table: "EventRegistrations",
                columns: new[] { "CareUserId", "EventSlotId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_EventSlots_EventSlotId",
                table: "EventRegistrations",
                column: "EventSlotId",
                principalTable: "EventSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
