using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class EChoice
    {
        public object ValueEnum { get; }
        public int ValueInt { get; }
        public string Name { get; }
        private Dictionary<string, string> Meta = new Dictionary<string, string>();

        public EChoice()
        {
        }

        public EChoice(object ValueEnum, string Name)
        {
            this.ValueEnum = ValueEnum;
            this.ValueInt = (int)ValueEnum;
            this.Name = Name;
        }

        public EChoice AddMeta(string key, string value)
        {
            Meta.Add(key, value);
            return this;
        }

        public string GetMeta(string key)
        {
            Meta.TryGetValue(key, out string value);
            return value;
        }

        public bool Equals(EChoice other)
        {
            return this.ValueInt.Equals(other.ValueInt);
        }
    }

    public class EChoicesUtil
    {
        public static EChoice Get(List<EChoice> Choices, object ValueEnum)
        {
            foreach (var eChoiceNaLista in Choices)
            {
                if (eChoiceNaLista.ValueEnum.Equals(ValueEnum))
                {
                    return eChoiceNaLista;
                }
            }

            return null;
        }

        public static EChoice Get(List<EChoice> Choices, int ValueInt)
        {
            foreach (var eChoiceNaLista in Choices)
            {
                if (eChoiceNaLista.ValueInt == ValueInt)
                {
                    return eChoiceNaLista;
                }
            }

            return null;
        }

        public static List<EChoice> Get(List<EChoice> Choices, string Name)
        {
            List<EChoice> results = new List<EChoice>();

            foreach (var eChoiceNaLista in Choices)
            {
                if (eChoiceNaLista.Name.Contains(Name))
                {
                    results.Add(eChoiceNaLista);
                }
            }

            return results;
        }
    }
}
