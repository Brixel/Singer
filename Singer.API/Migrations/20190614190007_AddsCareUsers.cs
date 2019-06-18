using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class AddsCareUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    CaseNumber = table.Column<string>(nullable: true),
                    AgeGroup = table.Column<int>(nullable: false),
                    IsExtern = table.Column<bool>(nullable: false),
                    HasTrajectory = table.Column<bool>(nullable: false),
                    HasNormalDayCare = table.Column<bool>(nullable: false),
                    HasVacationDayCare = table.Column<bool>(nullable: false),
                    HasResources = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareUsers_UserId",
                table: "CareUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareUsers");
        }
    }
}
