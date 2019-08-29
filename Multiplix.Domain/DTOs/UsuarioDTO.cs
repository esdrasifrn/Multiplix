using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string IdCateira { get; set; }
        public int PatrocinadorId { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; }

        public bool Liberado { get; set; }

        public IList<GrupoDTO> Grupos { get; set; } = new List<GrupoDTO>();

        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();

        // permissões
        public int? SuperUser { get; set; }
        public string GruposIDs { get; set; } // 1,2,100,...
        public List<PermissaoDTO> Permissoes { get; set; } = new List<PermissaoDTO>();
        public string PermissoesIDs { get; set; } // 1,2,100,...
    }
}
