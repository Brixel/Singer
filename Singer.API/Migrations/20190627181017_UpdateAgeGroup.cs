using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class UpdateAgeGroup : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("UPDATE CareUsers SET AgeGroup = 4 WHERE AgeGroup = 3");
        migrationBuilder.Sql("UPDATE CareUsers SET AgeGroup = 3 WHERE AgeGroup = 2");
        migrationBuilder.Sql("UPDATE CareUsers SET AgeGroup = 2 WHERE AgeGroup = 1");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
