using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class CorrectLegalGuardianCareUsersTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalGuardianCareUser_CareUsers_CareUserId",
                table: "LegalGuardianCareUser");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalGuardianCareUser_LegalGuardianUsers_LegalGuardianId",
                table: "LegalGuardianCareUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LegalGuardianCareUser",
                table: "LegalGuardianCareUser");

            migrationBuilder.RenameTable(
                name: "LegalGuardianCareUser",
                newName: "LegalGuardianCareUsers");

            migrationBuilder.RenameIndex(
                name: "IX_LegalGuardianCareUser_LegalGuardianId",
                table: "LegalGuardianCareUsers",
                newName: "IX_LegalGuardianCareUsers_LegalGuardianId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LegalGuardianCareUsers",
                table: "LegalGuardianCareUsers",
                columns: new[] { "CareUserId", "LegalGuardianId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LegalGuardianCareUsers_CareUsers_CareUserId",
                table: "LegalGuardianCareUsers",
                column: "CareUserId",
                principalTable: "CareUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalGuardianCareUsers_LegalGuardianUsers_LegalGuardianId",
                table: "LegalGuardianCareUsers",
                column: "LegalGuardianId",
                principalTable: "LegalGuardianUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalGuardianCareUsers_CareUsers_CareUserId",
                table: "LegalGuardianCareUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalGuardianCareUsers_LegalGuardianUsers_LegalGuardianId",
                table: "LegalGuardianCareUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LegalGuardianCareUsers",
                table: "LegalGuardianCareUsers");

            migrationBuilder.RenameTable(
                name: "LegalGuardianCareUsers",
                newName: "LegalGuardianCareUser");

            migrationBuilder.RenameIndex(
                name: "IX_LegalGuardianCareUsers_LegalGuardianId",
                table: "LegalGuardianCareUser",
                newName: "IX_LegalGuardianCareUser_LegalGuardianId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LegalGuardianCareUser",
                table: "LegalGuardianCareUser",
                columns: new[] { "CareUserId", "LegalGuardianId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LegalGuardianCareUser_CareUsers_CareUserId",
                table: "LegalGuardianCareUser",
                column: "CareUserId",
                principalTable: "CareUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalGuardianCareUser_LegalGuardianUsers_LegalGuardianId",
                table: "LegalGuardianCareUser",
                column: "LegalGuardianId",
                principalTable: "LegalGuardianUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
