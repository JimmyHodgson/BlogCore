using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class HomeModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "35d27c53-b532-472a-97d0-5a913d0425da", "9511e03f-524e-4379-8307-eece4b4c5868" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "87882e31-c1e2-4f34-9608-6181af0c2f98", "74642304-28b2-4126-886f-7e3e77489efa" });

            migrationBuilder.CreateTable(
                name: "Home",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LandingImage = table.Column<string>(nullable: true),
                    SkillImage = table.Column<string>(nullable: true),
                    ContactImage = table.Column<string>(nullable: true),
                    GithubLink = table.Column<string>(nullable: true),
                    LinkedInLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6911f789-3597-4027-ac51-0d631791fc61", "7a5b80c3-b816-464d-99b3-f33ef7ccb711", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46570737-e012-4a85-a0fd-c5c274440af2", "9acaa551-9b8f-47cf-91bb-e26f405d3d8a", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Home");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "46570737-e012-4a85-a0fd-c5c274440af2", "9acaa551-9b8f-47cf-91bb-e26f405d3d8a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6911f789-3597-4027-ac51-0d631791fc61", "7a5b80c3-b816-464d-99b3-f33ef7ccb711" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35d27c53-b532-472a-97d0-5a913d0425da", "9511e03f-524e-4379-8307-eece4b4c5868", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87882e31-c1e2-4f34-9608-6181af0c2f98", "74642304-28b2-4126-886f-7e3e77489efa", "Administrator", "ADMINISTRATOR" });
        }
    }
}
