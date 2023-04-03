using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Singer.Migrations.IdentityServer.PersistedGrantDb;

/// <inheritdoc />
public partial class Grants_v4 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "ConsumedTime",
            table: "PersistedGrants",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "PersistedGrants",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "SessionId",
            table: "PersistedGrants",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "DeviceCodes",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "SessionId",
            table: "DeviceCodes",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_Expiration",
            table: "PersistedGrants",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_SessionId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "SessionId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_Expiration",
            table: "DeviceCodes",
            column: "Expiration");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_PersistedGrants_Expiration",
            table: "PersistedGrants");

        migrationBuilder.DropIndex(
            name: "IX_PersistedGrants_SubjectId_SessionId_Type",
            table: "PersistedGrants");

        migrationBuilder.DropIndex(
            name: "IX_DeviceCodes_Expiration",
            table: "DeviceCodes");

        migrationBuilder.DropColumn(
            name: "ConsumedTime",
            table: "PersistedGrants");

        migrationBuilder.DropColumn(
            name: "Description",
            table: "PersistedGrants");

        migrationBuilder.DropColumn(
            name: "SessionId",
            table: "PersistedGrants");

        migrationBuilder.DropColumn(
            name: "Description",
            table: "DeviceCodes");

        migrationBuilder.DropColumn(
            name: "SessionId",
            table: "DeviceCodes");
    }
}
