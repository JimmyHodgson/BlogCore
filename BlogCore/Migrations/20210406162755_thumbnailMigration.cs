using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class thumbnailMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f30698d6-0269-4ac2-b902-510fcfbdda68", "c298cd6b-89ba-475b-84c3-b3239a4fd28a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fef26a8e-4605-4d20-ab32-d58a2e6558ca", "26c4a175-b4ba-491f-9589-10d546d7ade9" });

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "MediaLinks",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfd7d415-f8ce-486b-859d-446f434be52b", "3fa3aa4b-e538-40c9-8cf4-9482c997e24a", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2dac3e9b-a295-4ede-bcb5-05660d24f85f", "ef6b5fc1-3269-4562-8fb6-228a88a40f60", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2dac3e9b-a295-4ede-bcb5-05660d24f85f", "ef6b5fc1-3269-4562-8fb6-228a88a40f60" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "bfd7d415-f8ce-486b-859d-446f434be52b", "3fa3aa4b-e538-40c9-8cf4-9482c997e24a" });

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "MediaLinks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f30698d6-0269-4ac2-b902-510fcfbdda68", "c298cd6b-89ba-475b-84c3-b3239a4fd28a", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fef26a8e-4605-4d20-ab32-d58a2e6558ca", "26c4a175-b4ba-491f-9589-10d546d7ade9", "Administrator", "ADMINISTRATOR" });
        }
    }
}
