using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class PermissaoUsuario
    {
        public int PermissaoId { get; set; }
        public virtual Permissao Permissao { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
