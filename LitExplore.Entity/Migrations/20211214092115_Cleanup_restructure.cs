using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitExplore.Entity.Migrations
{
    public partial class Cleanup_restructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationReference");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Publications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "KeyWords",
                columns: table => new
                {
                    Keyword = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublicationTitle = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWords", x => x.Keyword);
                    table.ForeignKey(
                        name: "FK_KeyWords_Publications_PublicationTitle",
                        column: x => x.PublicationTitle,
                        principalTable: "Publications",
                        principalColumn: "Title");
                });

            migrationBuilder.CreateTable(
                name: "PublicationTitle",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublicationTitle = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTitle", x => x.Title);
                    table.ForeignKey(
                        name: "FK_PublicationTitle_Publications_PublicationTitle",
                        column: x => x.PublicationTitle,
                        principalTable: "Publications",
                        principalColumn: "Title");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_PublicationTitle",
                table: "KeyWords",
                column: "PublicationTitle");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTitle_PublicationTitle",
                table: "PublicationTitle",
                column: "PublicationTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "KeyWords");

            migrationBuilder.DropTable(
                name: "PublicationTitle");

            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Publications");

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "PublicationReference",
                columns: table => new
                {
                    PublicationsTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReferencesTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationReference", x => new { x.PublicationsTitle, x.ReferencesTitle });
                    table.ForeignKey(
                        name: "FK_PublicationReference_Publications_PublicationsTitle",
                        column: x => x.PublicationsTitle,
                        principalTable: "Publications",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationReference_References_ReferencesTitle",
                        column: x => x.ReferencesTitle,
                        principalTable: "References",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicationReference_ReferencesTitle",
                table: "PublicationReference",
                column: "ReferencesTitle");

            migrationBuilder.CreateIndex(
                name: "IX_References_Title",
                table: "References",
                column: "Title",
                unique: true);
        }
    }
}
