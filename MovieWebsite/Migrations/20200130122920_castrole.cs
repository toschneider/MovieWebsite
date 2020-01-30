using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieWebsite.Migrations
{
    public partial class castrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "ActorMovies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Character",
                table: "ActorMovies");
        }
    }
}
