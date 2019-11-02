using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Usuario
    {       
        private IList<UsuarioGrupo> _usuarioGrupos = new List<UsuarioGrupo>();
        private IList<PermissaoUsuario> _permissaoUsuarios = new List<PermissaoUsuario>();

        public Usuario(string login, string senha, string nome, string celular, string email, bool liberado)
        {
            Login = login;
            Senha = senha;
            Nome = nome;
            Celular = celular;
            Email = email;
            Liberado = liberado;                 
        }

        public Usuario()
        {
               
        }

        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime ValidadeSenha { get; set; }
        public bool Liberado { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public bool IsSuperUser { get; set; }
        public virtual ICollection<UsuarioGrupo> UsuarioGrupos { get => _usuarioGrupos; set { } }        
        public virtual ICollection<PermissaoUsuario> PermissaoUsuarios { get => _permissaoUsuarios; set { } }
       
        public void AddUsuarioGrupo(UsuarioGrupo usuarioGrupo)
        {
            _usuarioGrupos.Add(usuarioGrupo);
        }

        public void AddPermissaoUsuario(PermissaoUsuario permissaoUsuario)
        {
            _permissaoUsuarios.Add(permissaoUsuario);
        }
       
    }
}
