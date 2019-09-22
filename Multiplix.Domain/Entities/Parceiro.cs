using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Parceiro
    {
        public int ParceiroId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public String HorarioFuncionamento { get; set; }
        public virtual Endereco Endereco { get; set; }

        public Parceiro() { }

        public Parceiro(Usuario usuario, string horarioFuncionamento, Endereco endereco)
        {
            Usuario = usuario;
            HorarioFuncionamento = horarioFuncionamento;
            Endereco = endereco;
        }
    }
}
