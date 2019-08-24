using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Permissao
    {
        public virtual ICollection<PermissaoGrupo> PermissaoGrupos { get; set; }

        private IList<PermissaoUsuario> _permissaoUsuarios;

        public Permissao(string nomeId, string descricao)
        {
            NomeId = nomeId;
            Descricao = descricao;
            _permissaoUsuarios = new List<PermissaoUsuario>();
        }

        public int PermisaoId { get; set; }
        public string NomeId { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<PermissaoUsuario> PermissaoUsuarios { get => _permissaoUsuarios; private set { } }

        public void AddPermissaoUsuario(PermissaoUsuario permissaoUsuario)
        {
            _permissaoUsuarios.Add(permissaoUsuario);
        }
    }
}
