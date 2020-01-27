using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class datapagamentoNula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPagamento",
                table: "Entrada",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2020, 1, 26, 22, 52, 1, 198, DateTimeKind.Local).AddTicks(4720));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPagamento",
                table: "Entrada",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2020, 1, 26, 22, 37, 33, 209, DateTimeKind.Local).AddTicks(9738));
        }
    }
}
