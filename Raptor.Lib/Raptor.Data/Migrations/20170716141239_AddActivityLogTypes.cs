using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Raptor.Data.Migrations
{
    public partial class AddActivityLogTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityLogTypeId",
                table: "ActivityLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "ActivityLogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityLogTypes",
                columns: table => new
                {
                    ActivityLogTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    SystemKeyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogTypes", x => x.ActivityLogTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActivityLogTypeId",
                table: "ActivityLogs",
                column: "ActivityLogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_ActivityLogTypes_ActivityLogTypeId",
                table: "ActivityLogs",
                column: "ActivityLogTypeId",
                principalTable: "ActivityLogTypes",
                principalColumn: "ActivityLogTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_ActivityLogTypes_ActivityLogTypeId",
                table: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "ActivityLogTypes");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_ActivityLogTypeId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "ActivityLogTypeId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "ActivityLogs");
        }
    }
}
