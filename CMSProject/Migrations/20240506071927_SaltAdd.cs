using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class SaltAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTable_RoleTable",
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
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdRole",
                table: "UserTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "UserTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                oldMaxLength: 20,
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

            migrationBuilder.AddForeignKey(
                name: "FK_UserTable_RoleTable",
                table: "UserTable",
                column: "IdRole",
                principalTable: "RoleTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_FileTable_IdPublication",
                table: "FileTable",
                column: "IdPublication");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTable_IdUser",
                table: "PublicationTable",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_IdRole",
                table: "UserTable",
                column: "IdRole");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTable_RoleTable",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "UserTable");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserTable",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "IdRole",
                table: "UserTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserTable",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "RoleTable",
                type: "nchar(20)",
                fixedLength: true,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(20)",
                oldFixedLength: true,
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "IdPublication",
                table: "FileTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FileTable_PublicationTable",
                table: "FileTable",
                column: "IdPublication",
                principalTable: "PublicationTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTable_RoleTable",
                table: "UserTable",
                column: "IdRole",
                principalTable: "RoleTable",
                principalColumn: "Id");
        }
    }
}
