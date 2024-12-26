using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class PostTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFile",
                table: "PostingTables");

            migrationBuilder.DropColumn(
                name: "IdPublication",
                table: "PostingTables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
