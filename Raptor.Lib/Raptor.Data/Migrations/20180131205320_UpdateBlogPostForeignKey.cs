using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class UpdateBlogPostForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "BlogPostCategoryId",
                table: "BlogPosts",
                newName: "PostCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_PostCategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "PostCategoryId",
                table: "BlogPosts",
                newName: "BlogPostCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_PostCategoryId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_BlogPostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts",
                column: "BlogPostCategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "PostCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
