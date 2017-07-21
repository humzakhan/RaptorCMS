using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Raptor.Data.Migrations
{
    public partial class AddSettingsDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRole_People_BusinessEntityId",
                table: "PersonRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonRole_Roles_RoleId",
                table: "PersonRole");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonRole_BusinessEntityId_RoleId",
                table: "PersonRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonRole",
                table: "PersonRole");

            migrationBuilder.RenameTable(
                name: "PersonRole",
                newName: "PersonRoles");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonRoles_BusinessEntityId_RoleId",
                table: "PersonRoles",
                columns: new[] { "BusinessEntityId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonRoles",
                table: "PersonRoles",
                columns: new[] { "RoleId", "BusinessEntityId" });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRoles_People_BusinessEntityId",
                table: "PersonRoles",
                column: "BusinessEntityId",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRoles_Roles_RoleId",
                table: "PersonRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRoles_People_BusinessEntityId",
                table: "PersonRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonRoles_Roles_RoleId",
                table: "PersonRoles");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonRoles_BusinessEntityId_RoleId",
                table: "PersonRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonRoles",
                table: "PersonRoles");

            migrationBuilder.RenameTable(
                name: "PersonRoles",
                newName: "PersonRole");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonRole_BusinessEntityId_RoleId",
                table: "PersonRole",
                columns: new[] { "BusinessEntityId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonRole",
                table: "PersonRole",
                columns: new[] { "RoleId", "BusinessEntityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRole_People_BusinessEntityId",
                table: "PersonRole",
                column: "BusinessEntityId",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRole_Roles_RoleId",
                table: "PersonRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
