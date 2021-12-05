using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitExplore.Entity.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Title);
                });

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
                name: "IX_Publications_Title",
                table: "Publications",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_References_Title",
                table: "References",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationReference");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "References");
        }
    }
}
