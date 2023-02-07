using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTvMazeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TvMazeId",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TvMazeId",
                table: "Shows");
        }
    }
}
