using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum.Migrations
{
    /// <inheritdoc />
    public partial class GameGameCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameGameCategories",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameCategories", x => new { x.GameId, x.Category });
                    table.ForeignKey(
                        name: "FK_GameGameCategories_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGameCategories");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
