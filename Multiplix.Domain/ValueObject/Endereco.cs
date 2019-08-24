using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.ValueObject
{
    public class Endereco : ValueObject
    {
        public String Rua { get; private set; }
        public String Cidade { get; private set; }
        public String Estado { get; private set; }      
        public String CEP { get; private set; }
        public String Numero { get; private set; }       
      
        private Endereco(){ }

        public Endereco(string rua, string cidade, string estado, string cep, string numero)
        {
            Rua = rua;
            Cidade = cidade;
            Estado = estado;            
            CEP = cep;
            Numero = numero;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Rua;
            yield return Cidade;
            yield return Estado;           
            yield return CEP;
            yield return Numero;
        }
    }
}
