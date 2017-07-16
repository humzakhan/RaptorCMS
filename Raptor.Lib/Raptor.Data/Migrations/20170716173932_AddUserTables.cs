using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Raptor.Data.Migrations
{
    public partial class AddUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_PostCategories_PostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Customers_CustomerId",
                table: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ActivityLogs",
                newName: "BusinessEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityLogs_CustomerId",
                table: "ActivityLogs",
                newName: "IX_ActivityLogs_BusinessEntityId");

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Agent = table.Column<string>(maxLength: 255, nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    AuthorEmail = table.Column<string>(maxLength: 100, nullable: true),
                    AuthorIp = table.Column<string>(maxLength: 100, nullable: true),
                    AuthorUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedGmt = table.Column<DateTime>(nullable: false),
                    Karma = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_BlogComments_BlogPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "BlogPosts",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostCategories",
                columns: table => new
                {
                    PostCategoryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategories", x => x.PostCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "BusinessEntities",
                columns: table => new
                {
                    BusinessEntityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RowGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntities", x => x.BusinessEntityId);
                });

            migrationBuilder.CreateTable(
                name: "BusinessEntityAddresses",
                columns: table => new
                {
                    BusinessEntityId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    AddressType = table.Column<int>(nullable: false),
                    AddressTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEntityAddresses", x => new { x.BusinessEntityId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_BusinessEntityAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessEntityAddresses_BusinessEntities_BusinessEntityId",
                        column: x => x.BusinessEntityId,
                        principalTable: "BusinessEntities",
                        principalColumn: "BusinessEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    BusinessEntityId = table.Column<int>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    DateLastLoginUtc = table.Column<DateTime>(nullable: false),
                    DateModifiedUtc = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 180, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.BusinessEntityId);
                    table.ForeignKey(
                        name: "FK_People_BusinessEntities_BusinessEntityId",
                        column: x => x.BusinessEntityId,
                        principalTable: "BusinessEntities",
                        principalColumn: "BusinessEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_PostId",
                table: "BlogComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEntityAddresses_AddressId",
                table: "BusinessEntityAddresses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_PostCategoryId",
                table: "BlogPosts",
                column: "PostCategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "PostCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_BusinessEntities_BusinessEntityId",
                table: "ActivityLogs",
                column: "BusinessEntityId",
                principalTable: "BusinessEntities",
                principalColumn: "BusinessEntityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_PostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_BusinessEntities_BusinessEntityId",
                table: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "BlogPostCategories");

            migrationBuilder.DropTable(
                name: "BusinessEntityAddresses");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BusinessEntities");

            migrationBuilder.RenameColumn(
                name: "BusinessEntityId",
                table: "ActivityLogs",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityLogs_BusinessEntityId",
                table: "ActivityLogs",
                newName: "IX_ActivityLogs_CustomerId");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    CustomerType = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    FacebookUrl = table.Column<string>(nullable: true),
                    LinkedInUrl = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    TwitterUrl = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    PostCategoryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.PostCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    About = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(maxLength: 15, nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 152, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Location = table.Column<string>(maxLength: 20, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    IsAllowed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Agent = table.Column<string>(maxLength: 255, nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    AuthorEmail = table.Column<string>(maxLength: 100, nullable: true),
                    AuthorIp = table.Column<string>(maxLength: 100, nullable: true),
                    AuthorUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateCreatedGmt = table.Column<DateTime>(nullable: false),
                    Karma = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "BlogPosts",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<int>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.UniqueConstraint("AK_UserRoles_RoleId", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserProfileId",
                table: "Users",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId1",
                table: "UserRoles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_PostCategories_PostCategoryId",
                table: "BlogPosts",
                column: "PostCategoryId",
                principalTable: "PostCategories",
                principalColumn: "PostCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Customers_CustomerId",
                table: "ActivityLogs",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
