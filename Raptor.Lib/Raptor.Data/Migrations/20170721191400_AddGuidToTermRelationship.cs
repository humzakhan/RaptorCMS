using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Raptor.Data.Migrations
{
    public partial class AddGuidToTermRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TermRelationships",
                table: "TermRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "ObjectId",
                table: "TermRelationships",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "RowGuid",
                table: "TermRelationships",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TermRelationships",
                table: "TermRelationships",
                columns: new[] { "ObjectId", "TaxonomyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TermRelationships",
                table: "TermRelationships");

            migrationBuilder.DropColumn(
                name: "RowGuid",
                table: "TermRelationships");

            migrationBuilder.AlterColumn<int>(
                name: "ObjectId",
                table: "TermRelationships",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TermRelationships",
                table: "TermRelationships",
                column: "ObjectId");
        }
    }
}
