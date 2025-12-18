using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HytaleList_Backend_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedVotesAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "servers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "servers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "servers");

            migrationBuilder.DropColumn(
                name: "Votes",
                table: "servers");
        }
    }
}
