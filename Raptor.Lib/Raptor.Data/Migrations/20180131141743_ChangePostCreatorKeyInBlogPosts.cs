using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class ChangePostCreatorKeyInBlogPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BusinessEntities_CreatedById",
                table: "BlogPosts");

            migrationBuilder.AddColumn<int>(
                name: "BusinessEntityId",
                table: "BlogPosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BusinessEntityId",
                table: "BlogPosts",
                column: "BusinessEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BusinessEntities_BusinessEntityId",
                table: "BlogPosts",
                column: "BusinessEntityId",
                principalTable: "BusinessEntities",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_People_CreatedById",
                table: "BlogPosts",
                column: "CreatedById",
                principalTable: "People",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BusinessEntities_BusinessEntityId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_People_CreatedById",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BusinessEntityId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BusinessEntityId",
                table: "BlogPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BusinessEntities_CreatedById",
                table: "BlogPosts",
                column: "CreatedById",
                principalTable: "BusinessEntities",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
