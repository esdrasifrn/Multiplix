using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class ListaProdutoParceiroDTO
    {
        public ListaProdutoParceiroDTO(int parceiroId, string parceiro, string telefone, string endereco, string produto, Decimal preco, float pontosPorReal)
        {
            ParceiroId = parceiroId;
            Parceiro = parceiro;
            Telefone = telefone;
            Endereco = endereco;
            Produto = produto;
            Preco = preco;
            PontosPorReal = pontosPorReal;
        }

        public int ParceiroId { get; set; }
        public string Parceiro { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string  Produto { get; set; }
        public Decimal Preco { get; set; }
        public float PontosPorReal { get; set; }

    }
}
