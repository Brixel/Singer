using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class RemoveFaultyAPIResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql("DELETE FROM ApiScopes");
           migrationBuilder.Sql("DELETE FROM ApiResources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
