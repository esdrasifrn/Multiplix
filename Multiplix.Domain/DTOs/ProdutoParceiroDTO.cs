using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class ProdutoParceiroDTO
    {
        public int ParceiroId { get; set; }
        public IList<ProdutoDTO> Produtos { get; set; } = new List<ProdutoDTO>();
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
