using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class RemovesCaseNumberField : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CaseNumber",
            table: "CareUsers");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "CaseNumber",
            table: "CareUsers",
            nullable: true);
    }
}
