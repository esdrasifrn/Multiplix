using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class ParceiroProduto
    {
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int ParceiroId { get; set; }
        public virtual Parceiro Parceiro { get; set; }
        public float PontosPorRealProduto { get; set; }
        public float ValorProduto { get; set; }
    }
}
