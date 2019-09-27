using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class RamoAtividade
    {
        private IList<Parceiro>  _parceiros = new List<Parceiro>();
        public int RamoAtividadeId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Parceiro>  Parceiros { get => _parceiros; set { } }

        public RamoAtividade()
        {
                
        }
        public RamoAtividade(string nome)
        {
            Nome = nome;           
        }

        public void AddParceiro(Parceiro parceiro)
        {
            _parceiros.Add(parceiro);
        }
    }
}
