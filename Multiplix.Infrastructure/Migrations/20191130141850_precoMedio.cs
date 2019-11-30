using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class precoMedio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PrecoMedio",
                table: "Produto",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 30, 11, 18, 49, 690, DateTimeKind.Local).AddTicks(3781));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoMedio",
                table: "Produto");

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 24, 11, 23, 0, 443, DateTimeKind.Local).AddTicks(1831));
        }
    }
}
