﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
	public partial class AddCreatedColumn : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "Completed",
				table: "Mediators",
				type: "bit",
				nullable: false,
				defaultValue: false);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Completed",
				table: "Mediators");
		}
	}
}
