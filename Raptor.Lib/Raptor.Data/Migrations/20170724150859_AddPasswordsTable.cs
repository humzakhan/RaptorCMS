using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Raptor.Data.Migrations
{
    public partial class AddPasswordsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Password_People_BusinessEntityId",
                table: "Password");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Password",
                table: "Password");

            migrationBuilder.RenameTable(
                name: "Password",
                newName: "Passwords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passwords",
                table: "Passwords",
                column: "BusinessEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passwords_People_BusinessEntityId",
                table: "Passwords",
                column: "BusinessEntityId",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passwords_People_BusinessEntityId",
                table: "Passwords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passwords",
                table: "Passwords");

            migrationBuilder.RenameTable(
                name: "Passwords",
                newName: "Password");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Password",
                table: "Password",
                column: "BusinessEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Password_People_BusinessEntityId",
                table: "Password",
                column: "BusinessEntityId",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
