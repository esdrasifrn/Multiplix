using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class EstadoDTO
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
