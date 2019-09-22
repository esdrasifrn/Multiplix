using Multiplix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Enums
{
    public enum ETipoConta
    {
        CORRENTE = 0,
        POUPANCA = 1

    }

    public class ETipoContaChoices
    {
        public static List<EChoice> Choices = new List<EChoice>()
        {
            new EChoice(ETipoConta.CORRENTE, "Corrente"),
            new EChoice(ETipoConta.POUPANCA, "Poupança")
        };
    }
}
