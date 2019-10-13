using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class PlanoAssinaturaDTO
    {
        public int PlanoAssinaturaId { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set; }
        public string NomePlanoAssinatura { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
