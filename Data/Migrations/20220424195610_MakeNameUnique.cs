using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
	public partial class MakeNameUnique : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateIndex(
				name: "IX_Regions_Name_CityId",
				table: "Regions",
				columns: new[] { "Name", "CityId" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Governorates_Name",
				table: "Governorates",
				column: "Name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Cities_Name_GovernorateId",
				table: "Cities",
				columns: new[] { "Name", "GovernorateId" },
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_Regions_Name_CityId",
				table: "Regions");

			migrationBuilder.DropIndex(
				name: "IX_Governorates_Name",
				table: "Governorates");

			migrationBuilder.DropIndex(
				name: "IX_Cities_Name_GovernorateId",
				table: "Cities");
		}
	}
}
