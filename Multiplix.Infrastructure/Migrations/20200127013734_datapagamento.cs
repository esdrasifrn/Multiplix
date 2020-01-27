using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class datapagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataPagamento",
                table: "Entrada",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2020, 1, 26, 22, 37, 33, 209, DateTimeKind.Local).AddTicks(9738));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Entrada");

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2020, 1, 14, 18, 0, 54, 198, DateTimeKind.Local).AddTicks(8737));
        }
    }
}
