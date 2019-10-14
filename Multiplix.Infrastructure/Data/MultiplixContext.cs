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
                new Associado() { Id = 1, UsuarioId = 1, Nascimento = DateTime.Now, BancoId = 1, PlanoAssinaturaId = 1, TipoConta = 1, Estado = "RN", IdCarteira = "201900000001" }
                );

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
        }
    }
}