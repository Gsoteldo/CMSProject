using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class SaveFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserTable",
                type: "nchar(500)",
                fixedLength: true,
                maxLength: 500,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTable",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserTable",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50);

            //migrationBuilder.AddColumn<int>(
            //    name: "IdClient",
            //    table: "UserTable",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "RoleTable",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PublicationTable",
                type: "nchar(200)",
                fixedLength: true,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdPublication",
                table: "FileTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_IdClient",
                table: "UserTable",
                column: "IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable",
                column: "IdPublication",
                principalTable: "PublicationTable",
                principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserTable_UserTable",
            //    table: "UserTable",
            //    column: "IdClient",
            //    principalTable: "UserTable",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTable_UserTable",
                table: "UserTable");

            migrationBuilder.DropIndex(
                name: "IX_UserTable_IdClient",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "UserTable");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserTable",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(500)",
                oldFixedLength: true,
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "RoleTable",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PublicationTable",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(200)",
                oldFixedLength: true,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdPublication",
                table: "FileTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable",
                column: "IdPublication",
                principalTable: "PublicationTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
