using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class EducationLinkToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Links_LinkId",
                table: "Education");

            migrationBuilder.DropIndex(
                name: "IX_Education_LinkId",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "LinkId",
                table: "Education");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Education",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Education");

            migrationBuilder.AddColumn<Guid>(
                name: "LinkId",
                table: "Education",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Education_LinkId",
                table: "Education",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Links_LinkId",
                table: "Education",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
