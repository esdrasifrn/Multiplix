using Multiplix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Enums
{
    public enum EMes
    {
        JAN = 1,
        FEV = 2,
        MAR = 3,
        ABR = 4,
        MAI = 5,
        JUN = 6,
        JUL = 7,
        AGO = 8,
        SET = 9,
        OUT = 10,
        NOV = 11,
        DEZ = 12
    }

    public class EMesChoices
    {
        public static List<EChoice> Choices = new List<EChoice>()
        {
            new EChoice(EMes.JAN, "Janeiro"),
            new EChoice(EMes.FEV, "Fevereiro"),
            new EChoice(EMes.MAR, "Março"),
            new EChoice(EMes.ABR, "Abril"),
            new EChoice(EMes.MAI, "Maio"),
            new EChoice(EMes.JUN, "Junho"),
            new EChoice(EMes.JUL, "Julho"),
            new EChoice(EMes.AGO, "Agosto"),
            new EChoice(EMes.SET, "Setembro"),
            new EChoice(EMes.OUT, "Outubro"),
            new EChoice(EMes.NOV, "Novembro"),
            new EChoice(EMes.DEZ, "Dezembro")
        };
    }
}
