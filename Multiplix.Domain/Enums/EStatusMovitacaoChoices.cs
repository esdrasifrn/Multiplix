using Multiplix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Enums
{
    public enum EStatusMovimentacao
    {
        PENDENTE = 0,
        PAGO = 1,
        CORTESIA = 2

    }

    public class EStatusMovitacaoChoices
    {
        public static List<EChoice> Choices = new List<EChoice>()
        {
            new EChoice(EStatusMovimentacao.PENDENTE, "Pendente"),
            new EChoice(EStatusMovimentacao.PAGO, "Pago"),
            new EChoice(EStatusMovimentacao.CORTESIA, "Cortesia")
        };
    }
}
