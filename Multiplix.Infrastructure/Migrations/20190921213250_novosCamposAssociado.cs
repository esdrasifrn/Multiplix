using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class novosCamposAssociado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Associado",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Associado",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conta",
                table: "Associado",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Associado");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Associado");

            migrationBuilder.DropColumn(
                name: "Conta",
                table: "Associado");
        }
    }
}
