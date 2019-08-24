using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class PermissaoGrupo
    {
        public int PermissaoId { get; set; }
        public virtual Permissao Permissao { get; set; }
        public int GrupoId { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
