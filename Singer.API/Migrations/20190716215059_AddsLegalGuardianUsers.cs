using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class AddsLegalGuardianUsers : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "LegalGuardianUsers",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                UserId = table.Column<Guid>(nullable: false),
                Address = table.Column<string>(nullable: true),
                PostalCode = table.Column<string>(nullable: true),
                City = table.Column<string>(nullable: true),
                Country = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LegalGuardianUsers", x => x.Id);
                table.ForeignKey(
                    name: "FK_LegalGuardianUsers_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "LegalGuardianCareUser",
            columns: table => new
            {
                LegalGuardianId = table.Column<Guid>(nullable: false),
                CareUserId = table.Column<Guid>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LegalGuardianCareUser", x => new { x.CareUserId, x.LegalGuardianId });
                table.ForeignKey(
                    name: "FK_LegalGuardianCareUser_CareUsers_CareUserId",
                    column: x => x.CareUserId,
                    principalTable: "CareUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LegalGuardianCareUser_LegalGuardianUsers_LegalGuardianId",
                    column: x => x.LegalGuardianId,
                    principalTable: "LegalGuardianUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_LegalGuardianCareUser_LegalGuardianId",
            table: "LegalGuardianCareUser",
            column: "LegalGuardianId");

        migrationBuilder.CreateIndex(
            name: "IX_LegalGuardianUsers_UserId",
            table: "LegalGuardianUsers",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "LegalGuardianCareUser");

        migrationBuilder.DropTable(
            name: "LegalGuardianUsers");
    }
}
