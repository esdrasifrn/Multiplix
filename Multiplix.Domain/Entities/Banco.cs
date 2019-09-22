using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Banco
    {
        public int BancoId { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }

        public Banco()
        {
                
        }
        public Banco(string nome, string codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }
    }
}
