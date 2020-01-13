using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class EntradaDTO
    {
        public int EntradaId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }       
        public int Status { get; set; }

        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
