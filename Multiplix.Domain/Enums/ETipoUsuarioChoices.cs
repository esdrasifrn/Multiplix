using Multiplix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Enums
{
    public enum ETipoUsuario
    {
        ASSOCIADO = 0,
        PARCEIRO = 1

    }

    public class ETipoUsuarioChoices
    {
        public static List<EChoice> Choices = new List<EChoice>()
        {
            new EChoice(ETipoUsuario.ASSOCIADO, "Associado"),
            new EChoice(ETipoUsuario.PARCEIRO, "Parceiro")
        };
    }
}
