using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Bonus
    {
        public int BonusId { get; set; }
        public float Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual Associado AssociadoDono { get; set; } //dono do bonus
        public virtual Associado AssociadoGerador { get; set; } //quem gerou o bonus

        public Bonus()
        {

        }

        public Bonus(float valor, DateTime dataCadastro, Associado dono, Associado gerador)
        {
            Valor = valor;
            DataCadastro = dataCadastro;
            AssociadoDono = dono;
            AssociadoGerador = gerador;
        }
    }
}
