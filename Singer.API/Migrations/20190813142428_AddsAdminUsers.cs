using System;
using System.IO;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
    public partial class AddsAdminUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_UserId",
                table: "AdminUsers",
                column: "UserId");
            var sql = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, @"Migrations\Sql\CleanAdmin.sql"));
            migrationBuilder.Sql(sql);

      }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
