using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HouseholdDebtTrackerDAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    NickName = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    DebtorID = table.Column<int>(nullable: false),
                    CreditorID = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Debts_People_CreditorID",
                        column: x => x.CreditorID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debts_People_DebtorID",
                        column: x => x.DebtorID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debts_CreditorID",
                table: "Debts",
                column: "CreditorID");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_DebtorID",
                table: "Debts",
                column: "DebtorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
