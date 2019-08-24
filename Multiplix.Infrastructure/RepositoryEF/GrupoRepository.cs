using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Multiplix.Infrastructure.RepositoryEF
{
    public class GrupoRepository : EFRepository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(MultiplixContext multiplixContext, IConfiguration configuration) : base(multiplixContext, configuration)
        {
        }

        public void DeleteGrupoUsuarios(int grupoId)
        {
            var grupo = (from g in _dbContext.Grupos where g.GrupoId == grupoId select g).FirstOrDefault();

            var grupoUsuarios = grupo.UsuarioGrupos;

            foreach (var grupoUsuario in grupoUsuarios)
            {
                _dbContext.UsuarioGrupos.Remove(grupoUsuario);
            }

            _dbContext.SaveChanges();
        }

        public void DeletePermissoesGrupo(int grupoId)
        {
            var grupo = (from g in _dbContext.Grupos where g.GrupoId == grupoId select g).FirstOrDefault();

            var grupoPermissoes = grupo.PermissaoGrupos;

            foreach (var grupoPermissao in grupoPermissoes)
            {
                _dbContext.PermissaoGrupos.Remove(grupoPermissao);
            }

            _dbContext.SaveChanges();
        }
    }
}
