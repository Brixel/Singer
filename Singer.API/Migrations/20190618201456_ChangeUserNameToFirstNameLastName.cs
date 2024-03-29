﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations;

public partial class ChangeUserNameToFirstNameLastName : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Name",
            table: "AspNetUsers",
            newName: "LastName");

        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "AspNetUsers",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "AspNetUsers");

        migrationBuilder.RenameColumn(
            name: "LastName",
            table: "AspNetUsers",
            newName: "Name");
    }
}
