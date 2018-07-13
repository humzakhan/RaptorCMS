using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class RenameBlogPostIdColumnInComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_BlogPosts_PostId",
                table: "BlogComments");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "BlogComments",
                newName: "BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComments_PostId",
                table: "BlogComments",
                newName: "IX_BlogComments_BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_BlogPosts_BlogPostId",
                table: "BlogComments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_BlogPosts_BlogPostId",
                table: "BlogComments");

            migrationBuilder.RenameColumn(
                name: "BlogPostId",
                table: "BlogComments",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComments_BlogPostId",
                table: "BlogComments",
                newName: "IX_BlogComments_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_BlogPosts_PostId",
                table: "BlogComments",
                column: "PostId",
                principalTable: "BlogPosts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
