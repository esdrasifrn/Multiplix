﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Multiplix.Infrastructure.Data;

namespace Multiplix.Infrastructure.Migrations
{
    [DbContext(typeof(MultiplixContext))]
    partial class MultiplixContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Multiplix.Domain.Entities.Associado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Agencia");

                    b.Property<string>("Bairro");

                    b.Property<int?>("BancoId");

                    b.Property<string>("CEP")
                        .HasColumnName("CEP")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("CPF")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Cidade")
                        .HasColumnName("Cidade")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Complemento");

                    b.Property<string>("Conta");

                    b.Property<string>("EmailAlternativo");

                    b.Property<string>("Estado")
                        .HasColumnName("Estado")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("IdCarteira");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Numero")
                        .HasColumnName("Numero")
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("PatrocinadorId");

                    b.Property<string>("Rua")
                        .HasColumnName("Rua")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Sexo")
                        .HasColumnType("varchar(1)");

                    b.Property<int>("TipoConta");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("BancoId");

                    b.HasIndex("PatrocinadorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Associado");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Banco", b =>
                {
                    b.Property<int>("BancoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo");

                    b.Property<string>("Nome");

                    b.HasKey("BancoId");

                    b.ToTable("Banco");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Compra", b =>
                {
                    b.Property<int>("CompraId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssociadoId");

                    b.Property<DateTime>("Data");

                    b.Property<int?>("ParceiroId");

                    b.Property<float>("Pontos");

                    b.Property<float>("Valor");

                    b.HasKey("CompraId");

                    b.HasIndex("AssociadoId");

                    b.HasIndex("ParceiroId");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Grupo", b =>
                {
                    b.Property<int>("GrupoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome");

                    b.HasKey("GrupoId");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Parceiro", b =>
                {
                    b.Property<int>("ParceiroId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP")
                        .HasColumnName("CEP")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("CNPJ");

                    b.Property<string>("Cidade")
                        .HasColumnName("Cidade")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Complemento");

                    b.Property<string>("Estado")
                        .HasColumnName("Estado")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("HorarioFuncionamento")
                        .HasColumnName("HorarioFuncionamento")
                        .HasColumnType("varchar(75)");

                    b.Property<string>("Numero")
                        .HasColumnName("Numero")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("PontoPorReal");

                    b.Property<int?>("RamoAtividadeId");

                    b.Property<string>("Rua")
                        .HasColumnName("Rua")
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("ParceiroId");

                    b.HasIndex("RamoAtividadeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Parceiro");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.ParceiroProduto", b =>
                {
                    b.Property<int>("ParceiroId");

                    b.Property<int>("ProdutoId");

                    b.Property<float>("PontosPorRealProduto");

                    b.Property<float>("ValorProduto");

                    b.HasKey("ParceiroId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ParceiroProduto");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Permissao", b =>
                {
                    b.Property<int>("PermisaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NomeId")
                        .HasColumnType("varchar(250)");

                    b.HasKey("PermisaoId");

                    b.ToTable("Permissao");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.PermissaoGrupo", b =>
                {
                    b.Property<int>("PermissaoId");

                    b.Property<int>("GrupoId");

                    b.HasKey("PermissaoId", "GrupoId");

                    b.HasIndex("GrupoId");

                    b.ToTable("PermissaoGrupo");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.PermissaoUsuario", b =>
                {
                    b.Property<int>("PermissaoId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("PermissaoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PermissaoUsuario");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.HasKey("ProdutoId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.RamoAtividade", b =>
                {
                    b.Property<int>("RamoAtividadeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(150)");

                    b.HasKey("RamoAtividadeId");

                    b.ToTable("RamoDeAtividade");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Celular")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsSuperUser");

                    b.Property<bool>("Liberado");

                    b.Property<string>("Login")
                        .HasColumnType("varchar(55)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UltimoAcesso");

                    b.Property<DateTime>("ValidadeSenha");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            UsuarioId = 1,
                            Celular = "98776655",
                            Email = "admin@hotmail.com",
                            IsSuperUser = true,
                            Liberado = false,
                            Login = "admin",
                            Nome = "Admin",
                            Senha = "123",
                            UltimoAcesso = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidadeSenha = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.UsuarioGrupo", b =>
                {
                    b.Property<int>("UsuarioId");

                    b.Property<int>("GrupoId");

                    b.HasKey("UsuarioId", "GrupoId");

                    b.HasIndex("GrupoId");

                    b.ToTable("UsuarioGrupo");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Associado", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.Banco", "Banco")
                        .WithMany()
                        .HasForeignKey("BancoId");

                    b.HasOne("Multiplix.Domain.Entities.Associado")
                        .WithMany("Patrocinados")
                        .HasForeignKey("PatrocinadorId");

                    b.HasOne("Multiplix.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Compra", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.Associado", "Associado")
                        .WithMany("Compras")
                        .HasForeignKey("AssociadoId");

                    b.HasOne("Multiplix.Domain.Entities.Parceiro", "Parceiro")
                        .WithMany("Compras")
                        .HasForeignKey("ParceiroId");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.Parceiro", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.RamoAtividade", "Ramo")
                        .WithMany("Parceiros")
                        .HasForeignKey("RamoAtividadeId");

                    b.HasOne("Multiplix.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.ParceiroProduto", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.Parceiro", "Parceiro")
                        .WithMany("ParceiroProdutos")
                        .HasForeignKey("ParceiroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multiplix.Domain.Entities.Produto", "Produto")
                        .WithMany("ParceiroProdutos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.PermissaoGrupo", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.Grupo", "Grupo")
                        .WithMany("PermissaoGrupos")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multiplix.Domain.Entities.Permissao", "Permissao")
                        .WithMany("PermissaoGrupos")
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.PermissaoUsuario", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.Permissao", "Permissao")
                        .WithMany("PermissaoUsuarios")
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multiplix.Domain.Entities.Usuario", "Usuario")
                        .WithMany("PermissaoUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Multiplix.Domain.Entities.UsuarioGrupo", b =>
                {
                    b.HasOne("Multiplix.Domain.Entities.Grupo", "Grupo")
                        .WithMany("UsuarioGrupos")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Multiplix.Domain.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioGrupos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
