using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class MediaLinkMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c610ed57-6a93-4fa9-a70b-edcf2a0c7436", "9cbf86d5-de04-432c-98c4-465b83a289db" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d7872e2a-50cb-47df-89bf-5e77454fd59c", "6e83edd6-caf0-4c80-902c-0eaf5bb9f203" });

            migrationBuilder.CreateTable(
                name: "MediaLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Bucket = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaLinks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dda1026-45b2-43ff-906e-340d5e61b25a", "611b399a-da14-4d25-954a-21e1a52b025d", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7648fcb2-8ce6-4a58-855d-1e8100809e3c", "35e5a746-675a-4d11-82b8-3edfa21e13f2", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5dda1026-45b2-43ff-906e-340d5e61b25a", "611b399a-da14-4d25-954a-21e1a52b025d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7648fcb2-8ce6-4a58-855d-1e8100809e3c", "35e5a746-675a-4d11-82b8-3edfa21e13f2" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c610ed57-6a93-4fa9-a70b-edcf2a0c7436", "9cbf86d5-de04-432c-98c4-465b83a289db", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7872e2a-50cb-47df-89bf-5e77454fd59c", "6e83edd6-caf0-4c80-902c-0eaf5bb9f203", "Administrator", "ADMINISTRATOR" });
        }
    }
}
