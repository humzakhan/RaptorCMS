using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class AddForeignKeyToPersonsActivityLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "ActivityLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_PersonId",
                table: "ActivityLogs",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_People_PersonId",
                table: "ActivityLogs",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_People_PersonId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_PersonId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ActivityLogs");
        }
    }
}
