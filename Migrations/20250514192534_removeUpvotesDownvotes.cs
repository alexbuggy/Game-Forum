using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum.Migrations
{
    /// <inheritdoc />
    public partial class removeUpvotesDownvotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Upvotes",
                table: "Posts",
                newName: "VoteNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoteNumber",
                table: "Posts",
                newName: "Upvotes");

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
