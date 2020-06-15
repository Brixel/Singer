using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singer.Migrations
{
   public partial class RenamedEventLocationToSingerLocation : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_Events_EventLocations_LocationId",
             table: "Events");

         migrationBuilder.DropForeignKey(
             name: "FK_Registrations_EventLocations_DaycareLocationId",
             table: "Registrations");

         migrationBuilder.RenameTable(
            name: "EventLocations"
            , newName: "SingerLocations");

         migrationBuilder.DropPrimaryKey(
            name: "PK_EventLocations"
            , table: "SingerLocations");

         migrationBuilder.AddPrimaryKey(
            name: "PK_SingerLocations",
            table: "SingerLocations",
            column: "Id"
         );

         migrationBuilder.AddForeignKey(
             name: "FK_Events_SingerLocations_LocationId",
             table: "Events",
             column: "LocationId",
             principalTable: "SingerLocations",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);

         migrationBuilder.AddForeignKey(
             name: "FK_Registrations_SingerLocations_DaycareLocationId",
             table: "Registrations",
             column: "DaycareLocationId",
             principalTable: "SingerLocations",
             principalColumn: "Id",
             onDelete: ReferentialAction.Restrict);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         throw new NotImplementedException();
      }
   }
}
