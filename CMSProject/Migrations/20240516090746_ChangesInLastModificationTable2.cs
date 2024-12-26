using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInLastModificationTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PublicationTable_UserTable",
            //    table: "PublicationTable");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PublicationTable_UserTable",
            //    table: "PublicationTable",
            //    column: "IdUser",
            //    principalTable: "UserTable",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicationTable_UserTable",
                table: "PublicationTable");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationTable_UserTable",
                table: "PublicationTable",
                column: "IdUser",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
