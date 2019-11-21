using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Estado
    {
        private IList<Cidade> _cidades = new List<Cidade>();

        public int EstadoId { get; set; }       
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public virtual ICollection<Cidade> Cidades { get => _cidades; set { } }

        public Estado(string nome, string sigla)
        {
            Nome = nome;
            Sigla = sigla;
        }

        public Estado()
        {

        }

        public void AddCidade(Cidade cidade)
        {
            _cidades.Add(cidade);
        }
    }
}
