using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class PublicationIsPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyUser",
                table: "PublicationTable");

            migrationBuilder.DropColumn(
                name: "NameUser",
                table: "PublicationTable");

            migrationBuilder.AddColumn<bool>(
                name: "IsPost",
                table: "PublicationTable",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPost",
                table: "PublicationTable");

            migrationBuilder.AddColumn<string>(
                name: "CompanyUser",
                table: "PublicationTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameUser",
                table: "PublicationTable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
