using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class UpdateBlogPostsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "DateCreatedGmt",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "DateModifiedGmt",
                table: "BlogPosts",
                newName: "DateModifiedUtc");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "BlogPosts",
                newName: "DateCreatedUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModifiedUtc",
                table: "BlogPosts",
                newName: "DateModifiedGmt");

            migrationBuilder.RenameColumn(
                name: "DateCreatedUtc",
                table: "BlogPosts",
                newName: "DateModified");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "BlogPosts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreatedGmt",
                table: "BlogPosts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
