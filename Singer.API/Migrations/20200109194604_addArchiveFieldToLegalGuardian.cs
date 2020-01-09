using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class addArchiveFieldToLegalGuardian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "LegalGuardianUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "LegalGuardianUsers");
        }
    }
}
