using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class CidadeDTO
    {
        public int CidadeId { get; set; }
        public int EstadoId { get; set; }
        public string Descricao { get; set; }
        public string EstadoNome { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
