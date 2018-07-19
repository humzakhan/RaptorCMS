using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class RemoveBusinessEntityFromActivityLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_BusinessEntities_BusinessEntityId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_BusinessEntityId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "BusinessEntityId",
                table: "ActivityLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AddColumn<int>(
                name: "BusinessEntityId",
                table: "ActivityLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_BusinessEntityId",
                table: "ActivityLogs",
                column: "BusinessEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_BusinessEntities_BusinessEntityId",
                table: "ActivityLogs",
                column: "BusinessEntityId",
                principalTable: "BusinessEntities",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
