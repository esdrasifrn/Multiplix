using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class CompraItemDTO
    {
        public int CompraItemId { get; set; }
        public int Qtd { get; set; }
        public float ValorUnidade { get; set; }
        public float Subtotal { get; set; }
        public float SubtotalPontos { get; set; }
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
