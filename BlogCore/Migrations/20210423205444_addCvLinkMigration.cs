using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class addCvLinkMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "46570737-e012-4a85-a0fd-c5c274440af2", "9acaa551-9b8f-47cf-91bb-e26f405d3d8a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6911f789-3597-4027-ac51-0d631791fc61", "7a5b80c3-b816-464d-99b3-f33ef7ccb711" });

            migrationBuilder.AddColumn<string>(
                name: "CVLink",
                table: "Home",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6655fdd5-fdf1-4c22-bb1a-f6c163bebb80", "dff08efc-120c-4079-bf26-693af06cd84e", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95570dd9-8403-4f83-95db-ac1f87b66879", "6229bbe9-78f1-42da-9eef-4e2c80905abe", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6655fdd5-fdf1-4c22-bb1a-f6c163bebb80", "dff08efc-120c-4079-bf26-693af06cd84e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "95570dd9-8403-4f83-95db-ac1f87b66879", "6229bbe9-78f1-42da-9eef-4e2c80905abe" });

            migrationBuilder.DropColumn(
                name: "CVLink",
                table: "Home");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6911f789-3597-4027-ac51-0d631791fc61", "7a5b80c3-b816-464d-99b3-f33ef7ccb711", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46570737-e012-4a85-a0fd-c5c274440af2", "9acaa551-9b8f-47cf-91bb-e26f405d3d8a", "Administrator", "ADMINISTRATOR" });
        }
    }
}
