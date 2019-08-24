using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Associado
    {
        public int AssociadoId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public String HorarioFuncionamento { get; set; }
        public virtual Endereco Endereco { get; set; }

        public Associado() { }

        public Associado(Usuario usuario, string horarioFuncionamento, Endereco endereco)
        {
            Usuario = usuario;
            HorarioFuncionamento = horarioFuncionamento;
            Endereco = endereco;
        }
    }
}
