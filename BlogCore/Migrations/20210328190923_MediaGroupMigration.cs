using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class MediaGroupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5dda1026-45b2-43ff-906e-340d5e61b25a", "611b399a-da14-4d25-954a-21e1a52b025d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7648fcb2-8ce6-4a58-855d-1e8100809e3c", "35e5a746-675a-4d11-82b8-3edfa21e13f2" });

            migrationBuilder.DropColumn(
                name: "Bucket",
                table: "MediaLinks");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "MediaLinks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaGroups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f30698d6-0269-4ac2-b902-510fcfbdda68", "c298cd6b-89ba-475b-84c3-b3239a4fd28a", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fef26a8e-4605-4d20-ab32-d58a2e6558ca", "26c4a175-b4ba-491f-9589-10d546d7ade9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaLinks_GroupId",
                table: "MediaLinks",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaLinks_MediaGroups_GroupId",
                table: "MediaLinks",
                column: "GroupId",
                principalTable: "MediaGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaLinks_MediaGroups_GroupId",
                table: "MediaLinks");

            migrationBuilder.DropTable(
                name: "MediaGroups");

            migrationBuilder.DropIndex(
                name: "IX_MediaLinks_GroupId",
                table: "MediaLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f30698d6-0269-4ac2-b902-510fcfbdda68", "c298cd6b-89ba-475b-84c3-b3239a4fd28a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fef26a8e-4605-4d20-ab32-d58a2e6558ca", "26c4a175-b4ba-491f-9589-10d546d7ade9" });

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "MediaLinks");

            migrationBuilder.AddColumn<string>(
                name: "Bucket",
                table: "MediaLinks",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dda1026-45b2-43ff-906e-340d5e61b25a", "611b399a-da14-4d25-954a-21e1a52b025d", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7648fcb2-8ce6-4a58-855d-1e8100809e3c", "35e5a746-675a-4d11-82b8-3edfa21e13f2", "Administrator", "ADMINISTRATOR" });
        }
    }
}
