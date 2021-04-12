using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class removeLinkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2dac3e9b-a295-4ede-bcb5-05660d24f85f", "ef6b5fc1-3269-4562-8fb6-228a88a40f60" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "bfd7d415-f8ce-486b-859d-446f434be52b", "3fa3aa4b-e538-40c9-8cf4-9482c997e24a" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35d27c53-b532-472a-97d0-5a913d0425da", "9511e03f-524e-4379-8307-eece4b4c5868", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87882e31-c1e2-4f34-9608-6181af0c2f98", "74642304-28b2-4126-886f-7e3e77489efa", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    SkillModelId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Skills_SkillModelId",
                        column: x => x.SkillModelId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfd7d415-f8ce-486b-859d-446f434be52b", "3fa3aa4b-e538-40c9-8cf4-9482c997e24a", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2dac3e9b-a295-4ede-bcb5-05660d24f85f", "ef6b5fc1-3269-4562-8fb6-228a88a40f60", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Links_SkillModelId",
                table: "Links",
                column: "SkillModelId");
        }
    }
}
