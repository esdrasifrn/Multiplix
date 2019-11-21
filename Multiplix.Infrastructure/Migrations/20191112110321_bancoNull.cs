using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class bancoNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado");

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 12, 8, 3, 21, 214, DateTimeKind.Local).AddTicks(8281));

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "BancoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado");

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Associado",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 6, 17, 24, 28, 565, DateTimeKind.Local).AddTicks(6322));

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "BancoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
