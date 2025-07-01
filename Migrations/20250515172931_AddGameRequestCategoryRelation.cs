using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRequestCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRequests_AspNetUsers_RequestedByUserId1",
                table: "GameRequests");

            migrationBuilder.DropIndex(
                name: "IX_GameRequests_RequestedByUserId1",
                table: "GameRequests");

            migrationBuilder.DropColumn(
                name: "RequestedByUserId1",
                table: "GameRequests");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GameRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameRequestCategories",
                columns: table => new
                {
                    GameRequestId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRequestCategories", x => new { x.GameRequestId, x.Category });
                    table.ForeignKey(
                        name: "FK_GameRequestCategories_GameRequests_GameRequestId",
                        column: x => x.GameRequestId,
                        principalTable: "GameRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRequests_AspNetUsers_UserId",
                table: "GameRequests");

            migrationBuilder.DropTable(
                name: "GameRequestCategories");

            migrationBuilder.DropIndex(
                name: "IX_GameRequests_UserId",
                table: "GameRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameRequests");

            migrationBuilder.AddColumn<string>(
                name: "RequestedByUserId1",
                table: "GameRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GameRequests_RequestedByUserId1",
                table: "GameRequests",
                column: "RequestedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRequests_AspNetUsers_RequestedByUserId1",
                table: "GameRequests",
                column: "RequestedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
