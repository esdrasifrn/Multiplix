using Microsoft.EntityFrameworkCore;
using Multiplix.Domain.Entities;
using Multiplix.Infrastructure.EntityConfig;
using System;

namespace Multiplix.Infrastructure.Data
{
    public class MultiplixContext : DbContext
    {
        public MultiplixContext(DbContextOptions<MultiplixContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
       
        public DbSet<Grupo> Grupos { get; set; }     
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<PermissaoGrupo> PermissaoGrupos { get; set; }
        public DbSet<PermissaoUsuario> PermissaoUsuarios { get; set; }     
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioGrupo> UsuarioGrupos { get; set; }
        public DbSet<Associado> Patrocinadores { get; set; }
        public DbSet<Parceiro> Parceiros { get; set; }
        public DbSet<RamoAtividade> RamoAtividades { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ParceiroProduto>  parceiroProdutos { get; set; }
        public DbSet<PlanoAssinatura> planoAssinaturas { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario() {UsuarioId = 1, Login = "admin", Senha = "123", Nome = "Multiplys", Celular = "98776655", Email = "admin@hotmail.com", IsSuperUser = true }        
                );

            modelBuilder.Entity<Banco>().HasData(
                new Banco() { BancoId = 1, Codigo = "101", Nome = "Brasil" }
                );

            modelBuilder.Entity<PlanoAssinatura>().HasData(
                new PlanoAssinatura() { PlanoAssinaturaId = 1, Descricao = "Adesão I", Valor = 50 }
                );

            modelBuilder.Entity<Associado>().HasData(
                new Associado() { Id = 1, UsuarioId = 1, Nascimento = DateTime.Now, BancoId = 1, PlanoAssinaturaId = 1, TipoConta = 1, IdCarteira = "201900000001" }
                );

            //modelBuilder.Entity<Estado>().HasData(
            //   new Estado() { EstadoId = 1, Nome = "Acre", Sigla = "AC" },
            //   new Estado() { EstadoId = 2, Nome = "Alagoas", Sigla = "AL" },
            //   new Estado() { EstadoId = 3, Nome = "Amapá", Sigla = "AP" },
            //   new Estado() { EstadoId = 4, Nome = "Amazonas", Sigla = "AM" },
            //   new Estado() { EstadoId = 5, Nome = "Bahia", Sigla = "BA" },
            //   new Estado() { EstadoId = 6, Nome = "Ceará", Sigla = "CE" },
            //   new Estado() { EstadoId = 7, Nome = "Distrito Federal", Sigla = "DF" },
            //   new Estado() { EstadoId = 8, Nome = "Espírito Santo", Sigla = "ES" },
            //   new Estado() { EstadoId = 9, Nome = "Goiás", Sigla = "GO" },
            //   new Estado() { EstadoId = 10, Nome = "Maranhão", Sigla = "MA" },
            //   new Estado() { EstadoId = 11, Nome = "Mato Grosso", Sigla = "MT" },
            //   new Estado() { EstadoId = 12, Nome = "Mato Grosso do Sul", Sigla = "MS" },
            //   new Estado() { EstadoId = 13, Nome = "Minas Gerais", Sigla = "MG" },
            //   new Estado() { EstadoId = 14, Nome = "Pará", Sigla = "PA" },
            //   new Estado() { EstadoId = 15, Nome = "Paraíba", Sigla = "PB" },
            //   new Estado() { EstadoId = 16, Nome = "Paraná", Sigla = "PR" },
            //   new Estado() { EstadoId = 17, Nome = "Pernambuco", Sigla = "PE" },
            //   new Estado() { EstadoId = 18, Nome = "Piauí", Sigla = "PI" },
            //   new Estado() { EstadoId = 19, Nome = "Rio de Janeiro", Sigla = "RJ" },
            //   new Estado() { EstadoId = 20, Nome = "Rio Grande do Norte", Sigla = "RN" },
            //   new Estado() { EstadoId = 21, Nome = "Rio Grande do Sul", Sigla = "RS" },
            //   new Estado() { EstadoId = 22, Nome = "Rondônia", Sigla = "RO" },
            //   new Estado() { EstadoId = 23, Nome = "Roraima", Sigla = "RR" },
            //   new Estado() { EstadoId = 24, Nome = "Santa Catarina", Sigla = "SC" },
            //   new Estado() { EstadoId = 25, Nome = "São Paulo", Sigla = "SP" },
            //   new Estado() { EstadoId = 26, Nome = "Sergipe", Sigla = "SE" },
            //   new Estado() { EstadoId = 27, Nome = "Tocantins", Sigla = "TO" }
            //   );

            modelBuilder.Entity<Grupo>().ToTable("Grupo");          
            modelBuilder.Entity<Permissao>().ToTable("Permissao");
            modelBuilder.Entity<PermissaoGrupo>().ToTable("PermissaoGrupo");
            modelBuilder.Entity<PermissaoUsuario>().ToTable("PermissaoUsuario");          
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<UsuarioGrupo>().ToTable("UsuarioGrupo");
            modelBuilder.Entity<Associado>().ToTable("Associado");
            modelBuilder.Entity<Parceiro>().ToTable("Parceiro");
            modelBuilder.Entity<RamoAtividade>().ToTable("RamoDeAtividade");
            modelBuilder.Entity<Compra>().ToTable("Compra");
            modelBuilder.Entity<ParceiroProduto>().ToTable("ParceiroProduto");
            modelBuilder.Entity<PlanoAssinatura>().ToTable("PlanoAssinatura");
            modelBuilder.Entity<Cidade>().ToTable("Cidade");
            modelBuilder.Entity<Estado>().ToTable("Estado");

            modelBuilder.ApplyConfiguration(new GrupoMap());          
            modelBuilder.ApplyConfiguration(new PermissaoMap());           
            modelBuilder.ApplyConfiguration(new PermissaoUsuarioMap());
            modelBuilder.ApplyConfiguration(new PermissaoGrupoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioGrupoMap());
            modelBuilder.ApplyConfiguration(new AssociadoMap());
            modelBuilder.ApplyConfiguration(new ParceiroMap());
            modelBuilder.ApplyConfiguration(new RamoAtividadeMap());
            modelBuilder.ApplyConfiguration(new CompraMap());
            modelBuilder.ApplyConfiguration(new ParceiroProdutoMap());
            modelBuilder.ApplyConfiguration(new CidadeMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
        }
    }
}