using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class percentualRepasse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PercentualRepasseAtual",
                table: "ParceiroProduto",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PercentualRepasseEfetivado",
                table: "CompraItem",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ValorRepasse",
                table: "CompraItem",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 24, 11, 23, 0, 443, DateTimeKind.Local).AddTicks(1831));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentualRepasseAtual",
                table: "ParceiroProduto");

            migrationBuilder.DropColumn(
                name: "PercentualRepasseEfetivado",
                table: "CompraItem");

            migrationBuilder.DropColumn(
                name: "ValorRepasse",
                table: "CompraItem");

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 21, 18, 31, 3, 120, DateTimeKind.Local).AddTicks(3688));
        }
    }
}
