using Microsoft.Extensions.Configuration;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;
using System.Linq;

namespace Multiplix.Infrastructure.RepositoryEF
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MultiplixContext multiplixContext, IConfiguration configuration) : base(multiplixContext, configuration)
        {
              
        }

        public void DeleteUsuarioGrupos(int usuarioId)
        {
            var usuario = (from u in _dbContext.Usuarios where u.UsuarioId == usuarioId select u).FirstOrDefault();

            var usuarioGrupos = usuario.UsuarioGrupos;

            foreach (var usuarioGrupo in usuarioGrupos)
            {
                _dbContext.UsuarioGrupos.Remove(usuarioGrupo);
            }

            _dbContext.SaveChanges();
        }

        public void DeleteUsuarioPermissoes(int usuarioId)
        {
            var usuario = (from u in _dbContext.Usuarios where u.UsuarioId == usuarioId select u).FirstOrDefault();

            var usuarioPermissoes = usuario.PermissaoUsuarios;

            foreach (var usuarioPermissao in usuarioPermissoes)
            {
                _dbContext.PermissaoUsuarios.Remove(usuarioPermissao);
            }

            _dbContext.SaveChanges();
        }

        //public bool CPFJaExiste(string cpf)
        //{
        //    var cpfExiste = _dbContext.Usuarios.Where(x => x.CPF == cpf);

        //    return cpfExiste.Count() > 0;
        //}

        public bool LoginJaExiste(string login)
        {
            var loginExiste = _dbContext.Usuarios.Where(x => x.Login == login);

            return loginExiste.Count() > 0;
        }
    }
}
