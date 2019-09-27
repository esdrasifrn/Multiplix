using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class ramoAtividade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ramo",
                table: "Parceiro");

            migrationBuilder.AddColumn<int>(
                name: "RamoAtividadeId",
                table: "Parceiro",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RamoDeAtividade",
                columns: table => new
                {
                    RamoAtividadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RamoDeAtividade", x => x.RamoAtividadeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_RamoAtividadeId",
                table: "Parceiro",
                column: "RamoAtividadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parceiro_RamoDeAtividade_RamoAtividadeId",
                table: "Parceiro",
                column: "RamoAtividadeId",
                principalTable: "RamoDeAtividade",
                principalColumn: "RamoAtividadeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parceiro_RamoDeAtividade_RamoAtividadeId",
                table: "Parceiro");

            migrationBuilder.DropTable(
                name: "RamoDeAtividade");

            migrationBuilder.DropIndex(
                name: "IX_Parceiro_RamoAtividadeId",
                table: "Parceiro");

            migrationBuilder.DropColumn(
                name: "RamoAtividadeId",
                table: "Parceiro");

            migrationBuilder.AddColumn<string>(
                name: "Ramo",
                table: "Parceiro",
                nullable: true);
        }
    }
}
