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

            migrationBuilder.AddColumn<string>(
                name: "PublicationId",
                table: "KeyWords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicationId",
                table: "References",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_References",
                table: "References",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_PublicationId",
                table: "KeyWords",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_References_PublicationId",
                table: "References",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyWords_Publications_PublicationId",
                table: "KeyWords",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Title",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_References_Publications_PublicationId",
                table: "References",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Title",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyWords_Publications_PublicationId",
                table: "KeyWords");

            migrationBuilder.DropForeignKey(
                name: "FK_References_Publications_PublicationId",
                table: "References");

            migrationBuilder.DropIndex(
                name: "IX_KeyWords_PublicationId",
                table: "KeyWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_References",
                table: "References");

            migrationBuilder.DropIndex(
                name: "IX_References_PublicationId",
                table: "References");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "KeyWords");

            migrationBuilder.DropColumn(
                name: "PublicationId",
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
