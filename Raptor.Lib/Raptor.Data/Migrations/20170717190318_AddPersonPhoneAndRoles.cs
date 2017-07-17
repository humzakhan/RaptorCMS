using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Raptor.Data.Migrations
{
    public partial class AddPersonPhoneAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "SystemKeyword");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    BusinessEntityId = table.Column<int>(nullable: false),
                    RowGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRole", x => new { x.RoleId, x.BusinessEntityId });
                    table.UniqueConstraint("AK_PersonRole_BusinessEntityId_RoleId", x => new { x.BusinessEntityId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PersonRole_People_BusinessEntityId",
                        column: x => x.BusinessEntityId,
                        principalTable: "People",
                        principalColumn: "BusinessEntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    PhoneNumberId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    PhoneNumberType = table.Column<int>(nullable: false),
                    PhoneNumberTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.PhoneNumberId);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "BusinessEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonId",
                table: "PhoneNumber",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRole");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "SystemKeyword",
                table: "Roles",
                newName: "Name");
        }
    }
}
