using Microsoft.EntityFrameworkCore;
using Multiplix.Domain.Entities;
using Multiplix.Infrastructure.EntityConfig;

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
        public DbSet<UsuarioGrupo> Patrocinadores { get; set; }
        public DbSet<UsuarioGrupo> Associados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario() {UsuarioId = 1, Login = "admin", Senha = "123", Nome = "Admin", Celular = "98776655", Email = "admin@hotmail.com", IsSuperUser = true }
             );        

          
            modelBuilder.Entity<Grupo>().ToTable("Grupo");          
            modelBuilder.Entity<Permissao>().ToTable("Permissao");
            modelBuilder.Entity<PermissaoGrupo>().ToTable("PermissaoGrupo");
            modelBuilder.Entity<PermissaoUsuario>().ToTable("PermissaoUsuario");          
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<UsuarioGrupo>().ToTable("UsuarioGrupo");
            modelBuilder.Entity<Patrocinador>().ToTable("Patrocinador");
            modelBuilder.Entity<Associado>().ToTable("Associado");

            modelBuilder.ApplyConfiguration(new GrupoMap());          
            modelBuilder.ApplyConfiguration(new PermissaoMap());           
            modelBuilder.ApplyConfiguration(new PermissaoUsuarioMap());
            modelBuilder.ApplyConfiguration(new PermissaoGrupoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioGrupoMap());
            modelBuilder.ApplyConfiguration(new PatrocinadorMap());
            modelBuilder.ApplyConfiguration(new AssociadoMap());
        }
    }
}