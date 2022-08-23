using Microsoft.EntityFrameworkCore.Migrations;

namespace DancePlatform.Infrastructure.Migrations
{
    public partial class CountryAndCityChangeInTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
