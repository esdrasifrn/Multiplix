using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class configuracaoParceiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HorarioFuncionamento",
                table: "Parceiro",
                type: "varchar(75)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Parceiro",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Parceiro",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PontoPorReal",
                table: "Parceiro",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ramo",
                table: "Parceiro",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Parceiro");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Parceiro");

            migrationBuilder.DropColumn(
                name: "PontoPorReal",
                table: "Parceiro");

            migrationBuilder.DropColumn(
                name: "Ramo",
                table: "Parceiro");

            migrationBuilder.AlterColumn<string>(
                name: "HorarioFuncionamento",
                table: "Parceiro",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(75)",
                oldNullable: true);
        }
    }
}
