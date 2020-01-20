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
        public DateTime DataVencimento { get; set; }
        public int Status { get; set; }
        public int TipoEntrada { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float Valor { get; set; }

        public int AssociadoId { get; set; }
        public string NomeAssociado { get; set; }
        public int ParceiroId { get; set; }
        public string NomeParceiro { get; set; }


        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
