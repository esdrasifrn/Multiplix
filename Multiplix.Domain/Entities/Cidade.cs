using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Cidade
    {
        private IList<Parceiro> _parceiros = new List<Parceiro>();
        private IList<Associado> _associados = new List<Associado>();

        public int CidadeId { get; set; }       
        public string Descricao { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Parceiro> Parceiros { get => _parceiros; set { } }
        public virtual ICollection<Associado> Associados { get => _associados; set { } }

        public Cidade()
        {

        }

        public Cidade(string descricao, Estado estado)
        {
            Descricao = descricao;
            Estado = estado;
        }       

        public void AddParceiro(Parceiro parceiro)
        {
            _parceiros.Add(parceiro);
        }

        public void AddAssociado(Associado associado)
        {
            _associados.Add(associado);
        }
    }
}
