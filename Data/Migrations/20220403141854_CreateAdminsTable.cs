using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
	public partial class CreateAdminsTable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Admins",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
					PhoneNumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
					Email = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
					PasswordHash = table.Column<string>(type: "varchar(8000)", maxLength: 8000, nullable: false),
					Locked = table.Column<bool>(type: "bit", nullable: false),
					GenderId = table.Column<byte>(type: "tinyint", nullable: false),
					StatusId = table.Column<byte>(type: "tinyint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Admins", x => x.Id);
					table.ForeignKey(
						name: "FK_Admins_Genders_GenderId",
						column: x => x.GenderId,
						principalTable: "Genders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Admins_Status_StatusId",
						column: x => x.StatusId,
						principalTable: "Status",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "Locales",
				columns: new[] { "Id", "Name" },
				values: new object[] { (byte)1, "en" });

			migrationBuilder.CreateIndex(
				name: "IX_Admins_GenderId",
				table: "Admins",
				column: "GenderId");

			migrationBuilder.CreateIndex(
				name: "IX_Admins_StatusId",
				table: "Admins",
				column: "StatusId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Admins");

			migrationBuilder.DeleteData(
				table: "Locales",
				keyColumn: "Id",
				keyValue: (byte)1);
		}
	}
}
