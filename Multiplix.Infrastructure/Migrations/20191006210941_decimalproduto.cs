using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class decimalproduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorProduto",
                table: "ParceiroProduto",
                type: "decimal(15, 2)",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ValorProduto",
                table: "ParceiroProduto",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 2)");
        }
    }
}
