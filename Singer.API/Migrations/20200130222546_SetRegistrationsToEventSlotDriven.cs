using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class SetRegistrationsToEventSlotDriven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql("UPDATE Registrations SET [EventRegistrationType] = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
