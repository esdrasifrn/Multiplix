using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class ajusteAssociado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereco_Rua",
                table: "Associado",
                newName: "Rua");

            migrationBuilder.RenameColumn(
                name: "Endereco_Numero",
                table: "Associado",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Endereco_Estado",
                table: "Associado",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "Endereco_Cidade",
                table: "Associado",
                newName: "Cidade");

            migrationBuilder.RenameColumn(
                name: "Endereco_CEP",
                table: "Associado",
                newName: "CEP");

            migrationBuilder.AlterColumn<string>(
                name: "Rua",
                table: "Associado",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Associado",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Associado",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Associado",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "Associado",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rua",
                table: "Associado",
                newName: "Endereco_Rua");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Associado",
                newName: "Endereco_Numero");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Associado",
                newName: "Endereco_Estado");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Associado",
                newName: "Endereco_Cidade");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Associado",
                newName: "Endereco_CEP");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Rua",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Numero",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Estado",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Cidade",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_CEP",
                table: "Associado",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);
        }
    }
}
