using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class ModifyBlogCommentFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "AuthorEmail",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "AuthorUrl",
                table: "BlogComments");

            migrationBuilder.AddColumn<int>(
                name: "BusinessEntityId",
                table: "BlogComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BusinessEntityId",
                table: "BlogComments",
                column: "BusinessEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_People_BusinessEntityId",
                table: "BlogComments",
                column: "BusinessEntityId",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_People_BusinessEntityId",
                table: "BlogComments");

            migrationBuilder.DropIndex(
                name: "IX_BlogComments_BusinessEntityId",
                table: "BlogComments");

            migrationBuilder.DropColumn(
                name: "BusinessEntityId",
                table: "BlogComments");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BlogComments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorEmail",
                table: "BlogComments",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorUrl",
                table: "BlogComments",
                maxLength: 200,
                nullable: true);
        }
    }
}
