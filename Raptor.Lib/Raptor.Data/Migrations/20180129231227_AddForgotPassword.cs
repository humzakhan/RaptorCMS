using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Raptor.Data.Migrations
{
    public partial class AddForgotPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForgotPassqwordRequests",
                columns: table => new
                {
                    ForgotPasswordId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BusinessEntityId = table.Column<int>(nullable: false),
                    DateCreatedUtc = table.Column<DateTime>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPassqwordRequests", x => x.ForgotPasswordId);
                    table.ForeignKey(
                        name: "FK_ForgotPassqwordRequests_People_BusinessEntityId",
                        column: x => x.BusinessEntityId,
                        principalTable: "People",
                        principalColumn: "BusinessEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForgotPassqwordRequests_BusinessEntityId",
                table: "ForgotPassqwordRequests",
                column: "BusinessEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForgotPassqwordRequests");
        }
    }
}
