using Multiplix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Enums
{
    public enum ETipoEntrada
    {
        ADESAO = 0, // automática gerada pelo sistema
        REPASSE = 1, // automática gerada pelo sistema
        MANUAL = 2

    }

    public class ETipoEntradaChoices
    {
        public static List<EChoice> Choices = new List<EChoice>()
        {
            new EChoice(ETipoEntrada.ADESAO, "Adesão"),
            new EChoice(ETipoEntrada.REPASSE, "Repasse")
        };
    }
}
