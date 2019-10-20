using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class CompraDTO
    {
        public int CompraId { get; set; }
        public float Valor { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public float Pontos { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
        public IList<CompraItemDTO> CompraItems { get; set; } = new List<CompraItemDTO>();

        //Parceiro
        public int ParceiroId { get; set; }
        public string ParceiroNome { get; set; }

        //Associado
        public int AssociadoId { get; set; }
        public string AssociadoNome { get; set; }


    }
}
