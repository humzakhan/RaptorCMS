using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Raptor.Data.Migrations
{
    public partial class RenameBlogPostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CommentsCount = table.Column<int>(nullable: false),
                    CommentsStatus = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedGmt = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateModifiedGmt = table.Column<DateTime>(nullable: false),
                    Excerpt = table.Column<string>(nullable: true),
                    Guid = table.Column<Guid>(nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Password = table.Column<string>(maxLength: 20, nullable: true),
                    PostCategoryId = table.Column<int>(nullable: false),
                    PostType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.BlogPostId);
                    table.ForeignKey(
                        name: "FK_BlogPosts_PostCategories_PostCategoryId",
                        column: x => x.PostCategoryId,
                        principalTable: "PostCategories",
                        principalColumn: "PostCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_PostCategoryId",
                table: "BlogPosts",
                column: "PostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_PostId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CommentsCount = table.Column<int>(nullable: false),
                    CommentsStatus = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedGmt = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateModifiedGmt = table.Column<DateTime>(nullable: false),
                    Excerpt = table.Column<string>(nullable: true),
                    Guid = table.Column<Guid>(nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Password = table.Column<string>(maxLength: 20, nullable: true),
                    PostCategoryId = table.Column<int>(nullable: false),
                    PostType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_PostCategories_PostCategoryId",
                        column: x => x.PostCategoryId,
                        principalTable: "PostCategories",
                        principalColumn: "PostCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostCategoryId",
                table: "Posts",
                column: "PostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
