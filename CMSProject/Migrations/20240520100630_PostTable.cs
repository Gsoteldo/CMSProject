using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class PostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Media",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "PostAuthor",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "PostDescription",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "PostTitle",
                table: "PostingTables");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "PostingTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdFile",
                table: "PostingTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPublication",
                table: "PostingTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "PostingTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PostingTables_FileId",
                table: "PostingTables",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_PostingTables_PublicationId",
                table: "PostingTables",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingTables_FileTable_FileId",
                table: "PostingTables",
                column: "FileId",
                principalTable: "FileTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostingTables_PublicationTable_PublicationId",
                table: "PostingTables",
                column: "PublicationId",
                principalTable: "PublicationTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostingTables_FileTable_FileId",
                table: "PostingTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PostingTables_PublicationTable_PublicationId",
                table: "PostingTables");

            migrationBuilder.DropIndex(
                name: "IX_PostingTables_FileId",
                table: "PostingTables");

            migrationBuilder.DropIndex(
                name: "IX_PostingTables_PublicationId",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "IdFile",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "IdPublication",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "PostingTables");

            migrationBuilder.AddColumn<string>(
                name: "Media",
                table: "PostingTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostAuthor",
                table: "PostingTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "PostDate",
                table: "PostingTables",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "PostDescription",
                table: "PostingTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostTitle",
                table: "PostingTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
