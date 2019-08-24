using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class GrupoDTO
    {
        public int GrupoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IList<UsuarioDTO> Usuarios { get; set; } = new List<UsuarioDTO>();
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
        public string PermissoesIDs { get; set; } // 1,2,100,...
    }
}
