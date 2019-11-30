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

        //Um produto pode fazer parte de vários itens de compra
        private IList<CompraItem> _compraItems = new List<CompraItem>();

        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public float PrecoMedio { get; set; }
        public virtual ICollection<ParceiroProduto> ParceiroProdutos { get => _parceiroProdutos; set { } }
        public virtual ICollection<CompraItem> CompraItems { get => _compraItems; set { } }

        public Produto(string descricao, float precoMedio)
        {
            Descricao = descricao;
            PrecoMedio = precoMedio;
        }

        public Produto()
        {

        }

        public void AddParceiroProduto(ParceiroProduto parceiroProduto)
        {
            _parceiroProdutos.Add(parceiroProduto);
        }

        public void AddCompraItem(CompraItem compraItem)
        {
            _compraItems.Add(compraItem);
        }

    }
}
