using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1410209-9b5e-4984-a111-afa884bfdea0", "5d903c52-571c-428e-b495-22a49d3d64b0", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7df21873-a615-47a5-8f8b-a9c5a530aea5", "35be4899-f199-4924-af50-64adddbc6e95", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7df21873-a615-47a5-8f8b-a9c5a530aea5", "35be4899-f199-4924-af50-64adddbc6e95" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a1410209-9b5e-4984-a111-afa884bfdea0", "5d903c52-571c-428e-b495-22a49d3d64b0" });
        }
    }
}
