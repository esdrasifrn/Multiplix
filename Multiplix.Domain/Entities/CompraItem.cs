using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class CompraItem
    {
        public int CompraItemId { get; set; }
        public int Qtd { get; set; }
        public float ValorUnidade { get; set; }
        public float Subtotal { get; set; }
        public float SubtotalPontos { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Produto Produto { get; set; }
        public float PercentualRepasseEfetivado { get; set; }
        public float ValorRepasse { get; set; }


        public CompraItem(int qtd, float valorUnidade, Produto produto, float subtotal, float subtotalPontos)
        {
            Qtd = qtd;
            ValorUnidade = valorUnidade;
            Produto = produto;
            Subtotal = subtotal;
            SubtotalPontos = subtotalPontos;
        }

        public CompraItem()
        {

        }

    }
}
