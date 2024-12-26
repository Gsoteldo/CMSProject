using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInLastModificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PublicationTable_UserTable",
            //    table: "PublicationTable");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "LastModificationTable");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PublicationTable",
                type: "nchar(200)",
                fixedLength: true,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(200)",
                oldFixedLength: true,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PublicationDate",
                table: "PublicationTable",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "PublicationTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Concept",
                table: "PublicationTable",
                type: "nchar(1000)",
                fixedLength: true,
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(1000)",
                oldFixedLength: true,
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCreator",
                table: "LastModificationTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LastModificationTable_IdCreator",
                table: "LastModificationTable",
                column: "IdCreator");

            migrationBuilder.AddForeignKey(
                name: "FK_LastModificationTable_UserTable1",
                table: "LastModificationTable",
                column: "IdCreator",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationTable_UserTable",
                table: "PublicationTable",
                column: "IdUser",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LastModificationTable_UserTable1",
                table: "LastModificationTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationTable_UserTable",
                table: "PublicationTable");

            migrationBuilder.DropIndex(
                name: "IX_LastModificationTable_IdCreator",
                table: "LastModificationTable");

            migrationBuilder.DropColumn(
                name: "IdCreator",
                table: "LastModificationTable");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PublicationTable",
                type: "nchar(200)",
                fixedLength: true,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(200)",
                oldFixedLength: true,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PublicationDate",
                table: "PublicationTable",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "PublicationTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Concept",
                table: "PublicationTable",
                type: "nchar(1000)",
                fixedLength: true,
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(1000)",
                oldFixedLength: true,
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "LastModificationTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationTable_UserTable",
                table: "PublicationTable",
                column: "IdUser",
                principalTable: "UserTable",
                principalColumn: "Id");
        }
    }
}
