using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class DaycareLocationOnRegistration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CareUsers_EventLocations_NormalDaycareLocationId",
            table: "CareUsers");

        migrationBuilder.DropForeignKey(
            name: "FK_CareUsers_EventLocations_VacationDaycareLocationId",
            table: "CareUsers");

        migrationBuilder.DropIndex(
            name: "IX_CareUsers_NormalDaycareLocationId",
            table: "CareUsers");

        migrationBuilder.DropIndex(
            name: "IX_CareUsers_VacationDaycareLocationId",
            table: "CareUsers");

        migrationBuilder.DropColumn(
            name: "NormalDaycareLocationId",
            table: "CareUsers");

        migrationBuilder.DropColumn(
            name: "VacationDaycareLocationId",
            table: "CareUsers");

        migrationBuilder.AddColumn<Guid>(
            name: "DaycareLocationId",
            table: "EventRegistrations",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_EventRegistrations_DaycareLocationId",
            table: "EventRegistrations",
            column: "DaycareLocationId");

        migrationBuilder.AddForeignKey(
            name: "FK_EventRegistrations_EventLocations_DaycareLocationId",
            table: "EventRegistrations",
            column: "DaycareLocationId",
            principalTable: "EventLocations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_EventRegistrations_EventLocations_DaycareLocationId",
            table: "EventRegistrations");

        migrationBuilder.DropIndex(
            name: "IX_EventRegistrations_DaycareLocationId",
            table: "EventRegistrations");

        migrationBuilder.DropColumn(
            name: "DaycareLocationId",
            table: "EventRegistrations");

        migrationBuilder.AddColumn<Guid>(
            name: "NormalDaycareLocationId",
            table: "CareUsers",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "VacationDaycareLocationId",
            table: "CareUsers",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_CareUsers_NormalDaycareLocationId",
            table: "CareUsers",
            column: "NormalDaycareLocationId");

        migrationBuilder.CreateIndex(
            name: "IX_CareUsers_VacationDaycareLocationId",
            table: "CareUsers",
            column: "VacationDaycareLocationId");

        migrationBuilder.AddForeignKey(
            name: "FK_CareUsers_EventLocations_NormalDaycareLocationId",
            table: "CareUsers",
            column: "NormalDaycareLocationId",
            principalTable: "EventLocations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_CareUsers_EventLocations_VacationDaycareLocationId",
            table: "CareUsers",
            column: "VacationDaycareLocationId",
            principalTable: "EventLocations",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
