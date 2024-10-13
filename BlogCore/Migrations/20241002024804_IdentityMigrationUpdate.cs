using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogCore.Migrations
{
    /// <inheritdoc />
    public partial class IdentityMigrationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6655fdd5-fdf1-4c22-bb1a-f6c163bebb80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95570dd9-8403-4f83-95db-ac1f87b66879");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92e6c331-34aa-46ab-a4c8-4ae4cf1597d8", null, "Administrator", "ADMINISTRATOR" },
                    { "996b2ad3-b7a5-47e5-a567-4d1b5db7953b", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92e6c331-34aa-46ab-a4c8-4ae4cf1597d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "996b2ad3-b7a5-47e5-a567-4d1b5db7953b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6655fdd5-fdf1-4c22-bb1a-f6c163bebb80", "dff08efc-120c-4079-bf26-693af06cd84e", "Visitor", "VISITOR" },
                    { "95570dd9-8403-4f83-95db-ac1f87b66879", "6229bbe9-78f1-42da-9eef-4e2c80905abe", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
