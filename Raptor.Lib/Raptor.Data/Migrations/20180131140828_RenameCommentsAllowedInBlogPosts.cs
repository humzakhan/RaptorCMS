using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class RenameCommentsAllowedInBlogPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsStatus",
                table: "BlogPosts");

            migrationBuilder.AddColumn<bool>(
                name: "IsCommentsAllowed",
                table: "BlogPosts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCommentsAllowed",
                table: "BlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "CommentsStatus",
                table: "BlogPosts",
                nullable: true);
        }
    }
}
