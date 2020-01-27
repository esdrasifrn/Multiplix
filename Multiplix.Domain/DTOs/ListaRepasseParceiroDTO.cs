using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class ListaRepasseParceiroDTO
    {
        public ListaRepasseParceiroDTO(int parceiroId, string parceiro, Decimal valorRepasse, int numerovendas)
        {
            ParceiroId = parceiroId;
            Parceiro = parceiro;          
            ValorRepasse = valorRepasse;
            NumeroVendas = numerovendas;
        }

        public ListaRepasseParceiroDTO()
        {

        }

        public int ParceiroId { get; set; }
        public string Parceiro { get; set; }     
        public Decimal ValorRepasse { get; set; }
        public int NumeroVendas { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }


    }
}
