using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class addUserTitleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "55aa141f-b2ac-4f63-b580-d0f9fb995ec7", "04b471c3-a99b-4319-a6f3-8dbf2a239a97" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d644655e-18ef-4843-b113-be4adcf4f7ef", "b5f30348-afc6-47b7-b182-61a90ba50c98" });

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c610ed57-6a93-4fa9-a70b-edcf2a0c7436", "9cbf86d5-de04-432c-98c4-465b83a289db", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7872e2a-50cb-47df-89bf-5e77454fd59c", "6e83edd6-caf0-4c80-902c-0eaf5bb9f203", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c610ed57-6a93-4fa9-a70b-edcf2a0c7436", "9cbf86d5-de04-432c-98c4-465b83a289db" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d7872e2a-50cb-47df-89bf-5e77454fd59c", "6e83edd6-caf0-4c80-902c-0eaf5bb9f203" });

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d644655e-18ef-4843-b113-be4adcf4f7ef", "b5f30348-afc6-47b7-b182-61a90ba50c98", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55aa141f-b2ac-4f63-b580-d0f9fb995ec7", "04b471c3-a99b-4319-a6f3-8dbf2a239a97", "Administrator", "ADMINISTRATOR" });
        }
    }
}
