using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class bonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<float>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    AssociadoDonoId = table.Column<int>(nullable: true),
                    AssociadoGeradorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusId);
                    table.ForeignKey(
                        name: "FK_Bonus_Associado_AssociadoDonoId",
                        column: x => x.AssociadoDonoId,
                        principalTable: "Associado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonus_Associado_AssociadoGeradorId",
                        column: x => x.AssociadoGeradorId,
                        principalTable: "Associado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2020, 1, 11, 15, 59, 25, 881, DateTimeKind.Local).AddTicks(4712));

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_AssociadoDonoId",
                table: "Bonus",
                column: "AssociadoDonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_AssociadoGeradorId",
                table: "Bonus",
                column: "AssociadoGeradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.UpdateData(
                table: "Associado",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nascimento",
                value: new DateTime(2019, 11, 30, 11, 18, 49, 690, DateTimeKind.Local).AddTicks(3781));
        }
    }
}
