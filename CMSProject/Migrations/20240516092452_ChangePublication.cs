using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangePublication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "PublicationTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");


            //migrationBuilder.DropForeignKey(
            //    name: "FK_LastModificationTable_UserTable1",
            //    table: "LastModificationTable");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PublicationTable_UserTable",
            //    table: "UserTable");

            migrationBuilder.AddForeignKey(
                name: "FK_LastModificationTable_UserTable1",
                table: "LastModificationTable",
                column: "IdCreator",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PublicationTable_UserTable",
            //    table: "PublicationTable",
            //    column: "IdUser",
            //    principalTable: "UserTable",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "PublicationTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
