using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class dados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado");

            migrationBuilder.DropForeignKey(
                name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                table: "Associado");

            migrationBuilder.DropForeignKey(
                name: "FK_Associado_Usuario_UsuarioId",
                table: "Associado");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Associado",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlanoAssinaturaId",
                table: "Associado",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Associado",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Banco",
                columns: new[] { "BancoId", "Codigo", "Nome" },
                values: new object[] { 1, "101", "Brasil" });

            migrationBuilder.InsertData(
                table: "PlanoAssinatura",
                columns: new[] { "PlanoAssinaturaId", "Descricao", "Valor" },
                values: new object[] { 1, "Adesão I", 50f });

            migrationBuilder.InsertData(
                table: "Associado",
                columns: new[] { "Id", "Agencia", "Bairro", "BancoId", "CEP", "CPF", "Cidade", "Complemento", "Conta", "EmailAlternativo", "Estado", "IdCarteira", "Nascimento", "Nivel", "Numero", "PatrocinadorId", "PlanoAssinaturaId", "Rua", "Sexo", "TipoConta", "UsuarioId" },
                values: new object[] { 1, null, null, 1, null, null, null, null, null, null, "RN", null, new DateTime(2019, 10, 14, 9, 11, 41, 933, DateTimeKind.Local).AddTicks(2610), 0, null, null, 1, null, null, 1, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "BancoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                table: "Associado",
                column: "PlanoAssinaturaId",
                principalTable: "PlanoAssinatura",
                principalColumn: "PlanoAssinaturaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_Usuario_UsuarioId",
                table: "Associado",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado");

            migrationBuilder.DropForeignKey(
                name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                table: "Associado");

            migrationBuilder.DropForeignKey(
                name: "FK_Associado_Usuario_UsuarioId",
                table: "Associado");

            migrationBuilder.DeleteData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banco",
                keyColumn: "BancoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlanoAssinatura",
                keyColumn: "PlanoAssinaturaId",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PlanoAssinaturaId",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_Banco_BancoId",
                table: "Associado",
                column: "BancoId",
                principalTable: "Banco",
                principalColumn: "BancoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                table: "Associado",
                column: "PlanoAssinaturaId",
                principalTable: "PlanoAssinatura",
                principalColumn: "PlanoAssinaturaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_Usuario_UsuarioId",
                table: "Associado",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
