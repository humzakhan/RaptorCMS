using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Raptor.Data.Migrations
{
    public partial class AddCreatedByColumnToBlogPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_PostCategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "PostCategoryId",
                table: "BlogPosts",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_PostCategoryId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_CreatedById");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostCategoryId",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
                table: "BlogPosts",
                column: "BlogPostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts",
                column: "BlogPostCategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "PostCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BusinessEntities_CreatedById",
                table: "BlogPosts",
                column: "CreatedById",
                principalTable: "BusinessEntities",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BusinessEntities_CreatedById",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "BlogPosts",
                newName: "PostCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_CreatedById",
                table: "BlogPosts",
                newName: "IX_BlogPosts_PostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_PostCategoryId",
                table: "BlogPosts",
                column: "PostCategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "PostCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
