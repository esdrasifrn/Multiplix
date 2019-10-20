using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class decimalPontosPoreal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PontosPorRealProduto",
                table: "ParceiroProduto",
                type: "decimal(15, 2)",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 10, 20, 15, 16, 2, 642, DateTimeKind.Local).AddTicks(718));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PontosPorRealProduto",
                table: "ParceiroProduto",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 2)");

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 10, 14, 9, 20, 26, 770, DateTimeKind.Local).AddTicks(5106));
        }
    }
}
