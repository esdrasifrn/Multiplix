using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Multiplix.Infrastructure.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    BancoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.BancoId);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    EstadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Sigla = table.Column<string>(type: "varchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    GrupoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.GrupoId);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    PermisaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeId = table.Column<string>(type: "varchar(250)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.PermisaoId);
                });

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

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    PrecoMedio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ProdutoId);
                });

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

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "varchar(55)", nullable: true),
                    Senha = table.Column<string>(type: "varchar(50)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(25)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    ValidadeSenha = table.Column<DateTime>(nullable: false),
                    Liberado = table.Column<bool>(nullable: false),
                    UltimoAcesso = table.Column<DateTime>(nullable: false),
                    IsSuperUser = table.Column<bool>(nullable: false),
                    TipoUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    CidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    EstadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.CidadeId);
                    table.ForeignKey(
                        name: "FK_Cidade_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoGrupo",
                columns: table => new
                {
                    PermissaoId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoGrupo", x => new { x.PermissaoId, x.GrupoId });
                    table.ForeignKey(
                        name: "FK_PermissaoGrupo_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "GrupoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissaoGrupo_Permissao_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissao",
                        principalColumn: "PermisaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoUsuario",
                columns: table => new
                {
                    PermissaoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoUsuario", x => new { x.PermissaoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_PermissaoUsuario_Permissao_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissao",
                        principalColumn: "PermisaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissaoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioGrupo",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioGrupo", x => new { x.UsuarioId, x.GrupoId });
                    table.ForeignKey(
                        name: "FK_UsuarioGrupo_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "GrupoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Associado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nivel = table.Column<int>(nullable: false),
                    IdCarteira = table.Column<string>(nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false),
                    PlanoAssinaturaId = table.Column<int>(nullable: false),
                    PatrocinadorId = table.Column<int>(nullable: true),
                    Sexo = table.Column<string>(type: "varchar(1)", nullable: true),
                    CPF = table.Column<string>(type: "varchar(25)", nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    EmailAlternativo = table.Column<string>(nullable: true),
                    BancoId = table.Column<int>(nullable: true),
                    TipoConta = table.Column<int>(nullable: false),
                    Agencia = table.Column<string>(nullable: true),
                    Conta = table.Column<string>(nullable: true),
                    Rua = table.Column<string>(type: "varchar(200)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(15)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associado_Banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Banco",
                        principalColumn: "BancoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associado_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "CidadeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associado_Associado_PatrocinadorId",
                        column: x => x.PatrocinadorId,
                        principalTable: "Associado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associado_PlanoAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanoAssinatura",
                        principalColumn: "PlanoAssinaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Associado_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parceiro",
                columns: table => new
                {
                    ParceiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: true),
                    HorarioFuncionamento = table.Column<string>(type: "varchar(75)", nullable: true),
                    RamoAtividadeId = table.Column<int>(nullable: true),
                    CidadeId = table.Column<int>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    Responsavel = table.Column<string>(nullable: true),
                    Rua = table.Column<string>(type: "varchar(200)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(15)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiro", x => x.ParceiroId);
                    table.ForeignKey(
                        name: "FK_Parceiro_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "CidadeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parceiro_RamoDeAtividade_RamoAtividadeId",
                        column: x => x.RamoAtividadeId,
                        principalTable: "RamoDeAtividade",
                        principalColumn: "RamoAtividadeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parceiro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Entrada",
                columns: table => new
                {
                    EntradaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: true),
                    AssociadoId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TipoEntrada = table.Column<int>(nullable: false),
                    Valor = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrada", x => x.EntradaId);
                    table.ForeignKey(
                        name: "FK_Entrada_Associado_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "ParceiroProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    ParceiroId = table.Column<int>(nullable: false),
                    PontosPorRealProduto = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    ValorProduto = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    PercentualRepasseAtual = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParceiroProduto", x => new { x.ParceiroId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_ParceiroProduto_Parceiro_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "Parceiro",
                        principalColumn: "ParceiroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParceiroProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraItem",
                columns: table => new
                {
                    CompraItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Qtd = table.Column<int>(nullable: false),
                    ValorUnidade = table.Column<float>(nullable: false),
                    Subtotal = table.Column<float>(nullable: false),
                    SubtotalPontos = table.Column<float>(nullable: false),
                    CompraId = table.Column<int>(nullable: true),
                    ProdutoId = table.Column<int>(nullable: true),
                    PercentualRepasseEfetivado = table.Column<float>(nullable: false),
                    ValorRepasse = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraItem", x => x.CompraItemId);
                    table.ForeignKey(
                        name: "FK_CompraItem_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "CompraId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompraItem_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Banco",
                columns: new[] { "BancoId", "Codigo", "Nome" },
                values: new object[] { 1, "101", "Brasil" });

            migrationBuilder.InsertData(
                table: "PlanoAssinatura",
                columns: new[] { "PlanoAssinaturaId", "Descricao", "Valor" },
                values: new object[] { 1, "Adesão I", 50f });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "Celular", "Email", "IsSuperUser", "Liberado", "Login", "Nome", "Senha", "TipoUsuario", "UltimoAcesso", "ValidadeSenha" },
                values: new object[] { 1, "98776655", "admin@hotmail.com", true, false, "admin", "Multiplys", "123", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Associado",
                columns: new[] { "Id", "Agencia", "Bairro", "BancoId", "CEP", "CPF", "CidadeId", "Complemento", "Conta", "EmailAlternativo", "IdCarteira", "Nascimento", "Nivel", "Numero", "PatrocinadorId", "PlanoAssinaturaId", "Rua", "Sexo", "TipoConta", "UsuarioId" },
                values: new object[] { 1, null, null, 1, null, null, null, null, null, null, "201900000001", new DateTime(2020, 1, 27, 18, 48, 51, 320, DateTimeKind.Local).AddTicks(4962), 0, null, null, 1, null, null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Associado_BancoId",
                table: "Associado",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_CidadeId",
                table: "Associado",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_PatrocinadorId",
                table: "Associado",
                column: "PatrocinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_PlanoAssinaturaId",
                table: "Associado",
                column: "PlanoAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_UsuarioId",
                table: "Associado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_AssociadoDonoId",
                table: "Bonus",
                column: "AssociadoDonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_AssociadoGeradorId",
                table: "Bonus",
                column: "AssociadoGeradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_EstadoId",
                table: "Cidade",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_AssociadoId",
                table: "Compra",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_ParceiroId",
                table: "Compra",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItem_CompraId",
                table: "CompraItem",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItem_ProdutoId",
                table: "CompraItem",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_AssociadoId",
                table: "Entrada",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_CidadeId",
                table: "Parceiro",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_RamoAtividadeId",
                table: "Parceiro",
                column: "RamoAtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_UsuarioId",
                table: "Parceiro",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ParceiroProduto_ProdutoId",
                table: "ParceiroProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoGrupo_GrupoId",
                table: "PermissaoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissaoUsuario_UsuarioId",
                table: "PermissaoUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupo_GrupoId",
                table: "UsuarioGrupo",
                column: "GrupoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "CompraItem");

            migrationBuilder.DropTable(
                name: "Entrada");

            migrationBuilder.DropTable(
                name: "ParceiroProduto");

            migrationBuilder.DropTable(
                name: "PermissaoGrupo");

            migrationBuilder.DropTable(
                name: "PermissaoUsuario");

            migrationBuilder.DropTable(
                name: "UsuarioGrupo");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Associado");

            migrationBuilder.DropTable(
                name: "Parceiro");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "PlanoAssinatura");

            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "RamoDeAtividade");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
