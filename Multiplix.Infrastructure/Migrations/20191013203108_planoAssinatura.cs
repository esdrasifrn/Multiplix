using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class planoAssinatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanoAssinaturaId",
                table: "Associado",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlanoAssinatura",
                columns: table => new
                {
                    PlanoAssinaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<float>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoAssinatura", x => x.PlanoAssinaturaId);
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Nome",
                value: "Multiplys");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_PlanoAssinaturaId",
                table: "Associado",
                column: "PlanoAssinaturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                table: "Associado",
                column: "PlanoAssinaturaId",
                principalTable: "PlanoAssinatura",
                principalColumn: "PlanoAssinaturaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                table: "Associado");

            migrationBuilder.DropTable(
                name: "PlanoAssinatura");

            migrationBuilder.DropIndex(
                name: "IX_Associado_PlanoAssinaturaId",
                table: "Associado");

            migrationBuilder.DropColumn(
                name: "PlanoAssinaturaId",
                table: "Associado");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Nome",
                value: "Admin");
        }
    }
}
