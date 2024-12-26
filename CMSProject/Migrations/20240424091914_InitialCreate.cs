using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "RoleTable",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Role = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleTable", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserTable",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
            //        Email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
            //        Password = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
            //        IdRole = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserTable", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserTable_RoleTable",
            //            column: x => x.IdRole,
            //            principalTable: "RoleTable",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PublicationTable",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
            //        Concept = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
            //        PublicationDate = table.Column<DateOnly>(type: "date", nullable: true),
            //        IdUser = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PublicationTable", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PublicationTable_UserTable",
            //            column: x => x.IdUser,
            //            principalTable: "UserTable",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FileTable",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FileName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
            //        Type = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
            //        Path = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
            //        IdPublication = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FileTable", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_FileTable_PublicationTable",
            //            column: x => x.IdPublication,
            //            principalTable: "PublicationTable",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_FileTable_IdPublication",
            //    table: "FileTable",
            //    column: "IdPublication");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PublicationTable_IdUser",
            //    table: "PublicationTable",
            //    column: "IdUser");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserTable_IdRole",
            //    table: "UserTable",
            //    column: "IdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileTable");

            migrationBuilder.DropTable(
                name: "PublicationTable");

            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.DropTable(
                name: "RoleTable");
        }
    }
}
