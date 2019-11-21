using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class responsavel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PontoPorReal",
                table: "Parceiro");

            migrationBuilder.AddColumn<string>(
                name: "Responsavel",
                table: "Parceiro",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 21, 18, 31, 3, 120, DateTimeKind.Local).AddTicks(3688));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsavel",
                table: "Parceiro");

            migrationBuilder.AddColumn<int>(
                name: "PontoPorReal",
                table: "Parceiro",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 21, 18, 18, 12, 910, DateTimeKind.Local).AddTicks(9443));
        }
    }
}
