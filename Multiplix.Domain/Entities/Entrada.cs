using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Entrada
    {
        public int EntradaId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public virtual Associado Associado { get; set; }
        public int Status { get; set; }
        public int TipoEntrada { get; set; }
        public float Valor { get; set; }       
               

        public Entrada()
        {
                
        }

        public Entrada(string descricao, DateTime data, Associado associado, int status, float valor, DateTime dataVencimento, int tipoEntrada)
        {
            Descricao = descricao;
            Data = data;
            Associado = associado;
            Status = status;
            Valor = valor;
            DataVencimento = dataVencimento;
            TipoEntrada = tipoEntrada;
        }
    }
}