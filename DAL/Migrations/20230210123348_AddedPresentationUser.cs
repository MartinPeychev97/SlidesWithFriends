using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedPresentationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Presentations",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Presentations_UserId",
                table: "Presentations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presentations_AspNetUsers_UserId",
                table: "Presentations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presentations_AspNetUsers_UserId",
                table: "Presentations");

            migrationBuilder.DropIndex(
                name: "IX_Presentations_UserId",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Presentations");
        }
    }
}
