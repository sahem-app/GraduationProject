using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
	public partial class SeedAdminsTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "Admins",
				columns: new[] { "Id", "Email", "GenderId", "Locked", "Name", "PasswordHash", "PhoneNumber", "StatusId" },
				values: new object[] { 1, "admin@sahem.com", (byte)1, false, "Ahmed Medhat", "AQAAAAEAACcQAAAAEN51AJTAImssnM0ZiQGY0nRmJZui0akiWHdoGMaaa9QJ2zSVY1OgGzUeD6b2IdJRng==", "01068218987", (byte)2 });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Admins",
				keyColumn: "Id",
				keyValue: 1);
		}
	}
}
