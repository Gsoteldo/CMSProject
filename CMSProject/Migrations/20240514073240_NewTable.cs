using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class NewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTable",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserTable",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FileTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdPublication",
                table: "FileTable",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "FileTable",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LastModificationTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creator = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Publication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime", nullable: false),
                    Modification = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastModificationTable", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable",
                column: "IdPublication",
                principalTable: "PublicationTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable");

            migrationBuilder.DropTable(
                name: "LastModificationTable");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(100)",
                oldFixedLength: true,
                oldMaxLength: 100,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(100)",
                oldFixedLength: true,
                oldMaxLength: 100,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FileTable",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdPublication",
                table: "FileTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "FileTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(100)",
                oldFixedLength: true,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable",
                column: "IdPublication",
                principalTable: "PublicationTable",
                principalColumn: "Id");
        }
    }
}
