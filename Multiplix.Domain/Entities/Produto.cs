using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Produto
    {
        /// <summary>
        /// Um produto pode ter vários parceiros associados a ele
        /// </summary>
        private IList<ParceiroProduto> _parceiroProdutos = new List<ParceiroProduto>();

        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<ParceiroProduto> ParceiroProdutos { get => _parceiroProdutos; set { } }

        public Produto(string descricao)
        {
            Descricao = descricao;
        }

        public void AddParceiroProduto(ParceiroProduto parceiroProduto)
        {
            _parceiroProdutos.Add(parceiroProduto);
        }

    }
}
