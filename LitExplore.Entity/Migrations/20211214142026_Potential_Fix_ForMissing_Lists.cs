using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitExplore.Entity.Migrations
{
    public partial class Potential_Fix_ForMissing_Lists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyWords_Publications_PublicationTitle",
                table: "KeyWords");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicationTitle_Publications_PublicationTitle",
                table: "PublicationTitle");

            migrationBuilder.DropIndex(
                name: "IX_KeyWords_PublicationTitle",
                table: "KeyWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicationTitle",
                table: "PublicationTitle");

            migrationBuilder.DropIndex(
                name: "IX_PublicationTitle_PublicationTitle",
                table: "PublicationTitle");

            migrationBuilder.DropColumn(
                name: "PublicationTitle",
                table: "KeyWords");

            migrationBuilder.DropColumn(
                name: "PublicationTitle",
                table: "PublicationTitle");

            migrationBuilder.RenameTable(
                name: "PublicationTitle",
                newName: "References");

            migrationBuilder.AddPrimaryKey(
                name: "PK_References",
                table: "References",
                column: "Title");

            migrationBuilder.CreateTable(
                name: "KeyWordPublication",
                columns: table => new
                {
                    KeywordsKeyword = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublicationTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWordPublication", x => new { x.KeywordsKeyword, x.PublicationTitle });
                    table.ForeignKey(
                        name: "FK_KeyWordPublication_KeyWords_KeywordsKeyword",
                        column: x => x.KeywordsKeyword,
                        principalTable: "KeyWords",
                        principalColumn: "Keyword",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyWordPublication_Publications_PublicationTitle",
                        column: x => x.PublicationTitle,
                        principalTable: "Publications",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationPublicationTitle",
                columns: table => new
                {
                    PublicationTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReferencesTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationPublicationTitle", x => new { x.PublicationTitle, x.ReferencesTitle });
                    table.ForeignKey(
                        name: "FK_PublicationPublicationTitle_Publications_PublicationTitle",
                        column: x => x.PublicationTitle,
                        principalTable: "Publications",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationPublicationTitle_References_ReferencesTitle",
                        column: x => x.ReferencesTitle,
                        principalTable: "References",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyWordPublication_PublicationTitle",
                table: "KeyWordPublication",
                column: "PublicationTitle");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationPublicationTitle_ReferencesTitle",
                table: "PublicationPublicationTitle",
                column: "ReferencesTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyWordPublication");

            migrationBuilder.DropTable(
                name: "PublicationPublicationTitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_References",
                table: "References");

            migrationBuilder.RenameTable(
                name: "References",
                newName: "PublicationTitle");

            migrationBuilder.AddColumn<string>(
                name: "PublicationTitle",
                table: "KeyWords",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicationTitle",
                table: "PublicationTitle",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicationTitle",
                table: "PublicationTitle",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_PublicationTitle",
                table: "KeyWords",
                column: "PublicationTitle");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTitle_PublicationTitle",
                table: "PublicationTitle",
                column: "PublicationTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyWords_Publications_PublicationTitle",
                table: "KeyWords",
                column: "PublicationTitle",
                principalTable: "Publications",
                principalColumn: "Title");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicationTitle_Publications_PublicationTitle",
                table: "PublicationTitle",
                column: "PublicationTitle",
                principalTable: "Publications",
                principalColumn: "Title");
        }
    }
}
