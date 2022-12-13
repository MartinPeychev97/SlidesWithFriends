using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedTestEntityDataOnModelCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TestEntities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "firstTestEntity" });

            migrationBuilder.InsertData(
                table: "TestEntities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "secondTestEntity" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestEntities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestEntities",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
