using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
    public partial class AddfeildNameARInRegionAndCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                table: "Regions",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                table: "Cities",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name_AR_CityId",
                table: "Regions",
                columns: new[] { "Name_AR", "CityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_AR_GovernorateId",
                table: "Cities",
                columns: new[] { "Name_AR", "GovernorateId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Regions_Name_AR_CityId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_AR_GovernorateId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                table: "Cities");
        }
    }
}
