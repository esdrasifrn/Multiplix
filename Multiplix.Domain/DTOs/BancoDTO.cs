using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class BancoDTO
    {
        public int BancoId { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }       
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
