using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    /// <summary>
    /// Essa classe representa a transação de compra pelo associado ao parceiro da maultiplix
    /// </summary>
    public class Compra
    {
        public Compra(float valor, DateTime data, float pontos, Parceiro parceiro, Associado associado)
        {
            Valor = valor;
            Data = data;
            Pontos = pontos;
            Parceiro = parceiro;
            Associado = associado;
        }

        public Compra()
        {

        }

        public int CompraId { get; set; }
        public float Valor { get; set; }
        public DateTime Data { get; set; }
        public float Pontos { get; set; }

        //A compra é realizada pelo parceiro para o associado
        public virtual Parceiro Parceiro { get; set; }
        public virtual Associado Associado { get; set; }
    }
}
