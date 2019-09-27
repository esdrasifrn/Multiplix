using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class adicaodeCNPJ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Parceiro",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Parceiro");
        }
    }
}
