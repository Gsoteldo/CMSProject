using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class AddTinyMCE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostingTables_FileTable_FileId",
                table: "PostingTables");

            migrationBuilder.AlterColumn<string>(
                name: "Concept",
                table: "PublicationTable",
                type: "nvarchar(max)",
                fixedLength: true,
                maxLength: null,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(1000)",
                oldFixedLength: true,
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "PostingTables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PostingTables_FileTable_FileId",
                table: "PostingTables",
                column: "FileId",
                principalTable: "FileTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostingTables_FileTable_FileId",
                table: "PostingTables");

            migrationBuilder.AlterColumn<string>(
                name: "Concept",
                table: "PublicationTable",
                type: "nchar(1000)",
                fixedLength: true,
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(2147483647)",
                oldFixedLength: true,
                oldMaxLength: 2147483647);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "PostingTables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostingTables_FileTable_FileId",
                table: "PostingTables",
                column: "FileId",
                principalTable: "FileTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
