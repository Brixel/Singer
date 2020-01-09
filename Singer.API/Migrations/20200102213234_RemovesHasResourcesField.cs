using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class RemovesHasResourcesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasResources",
                table: "CareUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasResources",
                table: "CareUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
