using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Parceiro
    {
        /// <summary>
        /// Um Parceiro pode fazer várias vendas para um associado
        /// </summary>
        private IList<Compra> _compras = new List<Compra>();

        /// <summary>
        /// Um parceiro pode ter vários produtos associados a ele
        /// </summary>
        private IList<ParceiroProduto> _parceiroProdutos = new List<ParceiroProduto>();

        public int ParceiroId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public String HorarioFuncionamento { get; set; }
        public virtual RamoAtividade Ramo { get; set; }
        public int PontoPorReal { get; set; }
        public string CNPJ { get; set; }

        public virtual ICollection<Compra> Compras { get => _compras; set { } }
        public virtual ICollection<ParceiroProduto> ParceiroProdutos { get => _parceiroProdutos; set { } }


        //Endereço
        public String Rua { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public String CEP { get; set; }
        public String Numero { get; set; }
        public String Bairro { get; set; }
        public String Complemento { get; set; }

        public Parceiro() { }

        public Parceiro(Usuario usuario, string horarioFuncionamento, string rua, string numero, string cep, string cidade,
            string bairro, string complemento, string estado, RamoAtividade ramo, int pontoPorReal, string cnpj)
        {
            Usuario = usuario;
            HorarioFuncionamento = horarioFuncionamento;
            Rua = rua;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Ramo = ramo;
            PontoPorReal = pontoPorReal;
            CNPJ = cnpj;
        }

        public void AddCompra(Compra compra)
        {
            _compras.Add(compra);
        }

        public void AddProdutoParceiro(ParceiroProduto parceiroProduto)
        {
            _parceiroProdutos.Add(parceiroProduto);
        }
    }
}
