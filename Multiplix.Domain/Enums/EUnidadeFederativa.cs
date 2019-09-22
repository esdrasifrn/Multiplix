using System.Collections.Generic;

namespace Multiplix.Domain.Enums
{
    public class EUnidadeFederativa
    {
        public static Dictionary<string, string> All()
        {
            Dictionary<string, string> ufs = new Dictionary<string, string>
            {
                {"", ""},
                {"AC", "Acre"},
                {"AL", "Alagoas"},
                {"AP", "Amapá"},
                {"AM", "Amazonas"},
                {"BA", "Bahia"},
                {"CE", "Ceará"},
                {"DF", "Distrito Federal"},
                {"ES", "Espírito Santo"},
                {"GO", "Goiás"},
                {"MA", "Maranhão"},
                {"MT", "Mato Grosso"},
                {"MS", "Mato Grosso do Sul"},
                {"MG", "Minas Gerais"},
                {"PA", "Pará"},
                {"PB", "Paraíba"},
                {"PR", "Paraná"},
                {"PE", "Pernambuco"},
                {"PI", "Piauí"},
                {"RJ", "Rio de Janeiro"},
                {"RN", "Rio Grande do Norte"},
                {"RS", "Rio Grande do Sul"},
                {"RO", "Rondônia"},
                {"RR", "Roraima"},
                {"SC", "Santa Catarina"},
                {"SP", "São Paulo"},
                {"SE", "Sergipe"},
                {"TO", "Tocantins"}
            };
            return ufs;
        }

        public static string getDescricao(string sigla)
        {
            foreach(var estado in EUnidadeFederativa.All())
            {
                if (estado.Key == sigla)
                {
                    return estado.Value;
                }
            }
            return "";
        }
    }
}
