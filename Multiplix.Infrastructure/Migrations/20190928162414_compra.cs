using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class compra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    CompraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<float>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Pontos = table.Column<float>(nullable: false),
                    ParceiroId = table.Column<int>(nullable: true),
                    AssociadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.CompraId);
                    table.ForeignKey(
                        name: "FK_Compra_Associado_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compra_Parceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiro",
                        principalColumn: "ParceiroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_AssociadoId",
                table: "Compra",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_ParceiroId",
                table: "Compra",
                column: "ParceiroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");
        }
    }
}
