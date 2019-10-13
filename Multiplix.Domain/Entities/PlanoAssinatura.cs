using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class PlanoAssinatura
    {
        private IList<Associado> _associados = new List<Associado>();

        public int PlanoAssinaturaId { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Associado> Associados { get => _associados; set { } }

        public PlanoAssinatura(float valor, string descricao)
        {
            Valor = valor;
            Descricao = descricao;
        }

        public void AddAssociado(Associado associado)
        {
            _associados.Add(associado);
        }
    }
}
