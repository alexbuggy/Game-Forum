using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRequests_AspNetUsers_UserId",
                table: "GameRequests");

            migrationBuilder.DropIndex(
                name: "IX_GameRequests_UserId",
                table: "GameRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GameRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRequests_UserId",
                table: "GameRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRequests_AspNetUsers_UserId",
                table: "GameRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
