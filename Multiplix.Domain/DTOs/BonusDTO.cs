using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class BonusDTO
    {
        public int BonusId { get; set; }
        public float Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
