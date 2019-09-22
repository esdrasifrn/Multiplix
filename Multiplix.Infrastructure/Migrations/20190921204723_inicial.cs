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
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "varchar(15)", nullable: true),
                    Senha = table.Column<string>(type: "varchar(50)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(25)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    ValidadeSenha = table.Column<DateTime>(nullable: false),
                    Liberado = table.Column<bool>(nullable: false),
                    UltimoAcesso = table.Column<DateTime>(nullable: false),
                    IsSuperUser = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
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
                name: "Associado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCarteira = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: true),
                    PatrocinadorId = table.Column<int>(nullable: true),
                    Sexo = table.Column<string>(type: "varchar(1)", nullable: true),
                    CPF = table.Column<string>(type: "varchar(25)", nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    EmailAlternativo = table.Column<string>(nullable: true),
                    BancoId = table.Column<int>(nullable: true),
                    TipoConta = table.Column<int>(nullable: false),
                    Agencia = table.Column<string>(nullable: true),
                    Rua = table.Column<string>(type: "varchar(200)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(200)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(15)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true)
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
                        name: "FK_Associado_Associado_PatrocinadorId",
                        column: x => x.PatrocinadorId,
                        principalTable: "Associado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associado_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parceiro",
                columns: table => new
                {
                    ParceiroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: true),
                    HorarioFuncionamento = table.Column<string>(nullable: true),
                    Rua = table.Column<string>(type: "varchar(200)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(200)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(15)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiro", x => x.ParceiroId);
                    table.ForeignKey(
                        name: "FK_Parceiro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioId", "Celular", "Email", "IsSuperUser", "Liberado", "Login", "Nome", "Senha", "UltimoAcesso", "ValidadeSenha" },
                values: new object[] { 1, "98776655", "admin@hotmail.com", true, false, "admin", "Admin", "123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Associado_BancoId",
                table: "Associado",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_PatrocinadorId",
                table: "Associado",
                column: "PatrocinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Associado_UsuarioId",
                table: "Associado",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiro_UsuarioId",
                table: "Parceiro",
                column: "UsuarioId");

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
                name: "Associado");

            migrationBuilder.DropTable(
                name: "Parceiro");

            migrationBuilder.DropTable(
                name: "PermissaoGrupo");

            migrationBuilder.DropTable(
                name: "PermissaoUsuario");

            migrationBuilder.DropTable(
                name: "UsuarioGrupo");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
