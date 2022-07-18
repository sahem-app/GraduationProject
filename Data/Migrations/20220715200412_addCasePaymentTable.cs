using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GraduationProject.Migrations
{
    public partial class addCasePaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Casepayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    MediatorId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RoundNnumber = table.Column<int>(type: "int", nullable: true),
                    TransactionImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    DateDelivered = table.Column<DateTime>(type: "datetime2(0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casepayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Casepayment_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Casepayment_Mediators_MediatorId",
                        column: x => x.MediatorId,
                        principalTable: "Mediators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Casepayment_CaseId",
                table: "Casepayment",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Casepayment_MediatorId",
                table: "Casepayment",
                column: "MediatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Casepayment");
        }
    }
}
