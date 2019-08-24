using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Grupo
    {
        private IList<UsuarioGrupo> _usuarioGrupos;
        private IList<PermissaoGrupo> _permissaoGrupos;

        public Grupo(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            _usuarioGrupos = new List<UsuarioGrupo>();
            _permissaoGrupos = new List<PermissaoGrupo>();
        }

        public int GrupoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<PermissaoGrupo> PermissaoGrupos { get => _permissaoGrupos; set { } }
        public virtual ICollection<UsuarioGrupo> UsuarioGrupos { get => _usuarioGrupos; set { } }

        public void AddPermissaoGrupo(PermissaoGrupo permissaoGrupo)
        {
            _permissaoGrupos.Add(permissaoGrupo);
        }

        public void AddUsuarioGrupo(UsuarioGrupo usuarioGrupo)
        {
            _usuarioGrupos.Add(usuarioGrupo);
        }
    }
}
