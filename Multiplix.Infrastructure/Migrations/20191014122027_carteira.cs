using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class carteira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdCarteira", "Nascimento" },
                values: new object[] { "201900000001", new DateTime(2019, 10, 14, 9, 20, 26, 770, DateTimeKind.Local).AddTicks(5106) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdCarteira", "Nascimento" },
                values: new object[] { null, new DateTime(2019, 10, 14, 9, 11, 41, 933, DateTimeKind.Local).AddTicks(2610) });
        }
    }
}
