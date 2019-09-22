using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Associado
    {   
        /// <summary>
        /// Um patrocinador pode ter vários patrocinados (auto-relacionamento)
        /// Patrocinador é quem faz o convite e patrocinado é quem recebe o convite
        /// </summary>
        private IList<Associado> _patrocinados;

        public Associado(Usuario usuario, int? patrocinadorId, string rua, string numero, string cep,  string cidade, 
            string bairro, string complemento, string estado, DateTime nascimento,
            string sexo, string cpf, string emailAlternativo, Banco banco, int tipoConta, string agengia, string conta)
        {
            Usuario = usuario;
            PatrocinadorId = patrocinadorId;
            IdCarteira = $"{DateTime.Now.Year.ToString()}{Id:00000000}";
            _patrocinados = new List<Associado>();
            Rua = rua;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Nascimento = nascimento;
            Sexo = sexo;
            CPF = cpf;
            EmailAlternativo = emailAlternativo;
            Banco = banco;
            TipoConta = tipoConta;
            Agencia = agengia;
            Conta = conta;
        }

        public Associado()
        {
            _patrocinados = new List<Associado>();
        }

        public int Id { get; set; }
        public string IdCarteira { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int? PatrocinadorId { get; set; }       
        public string Sexo { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }

        public string EmailAlternativo { get; set; }

        public virtual Banco Banco { get; set; }
        public int TipoConta { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }


        public String Rua { get;  set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public String CEP { get; set; }
        public String Numero { get; set; }
        public String Bairro { get; set; }
        public String Complemento { get; set; }


        public virtual ICollection<Associado> Patrocinados { get => _patrocinados; set { } }             

        /// <summary>
        /// Adiciona o associado patrocinado ao associado patrocinador que fez o convite
        /// </summary>
        /// <param name="patrocinado"></param>
        public void AddPatrocinado(Associado patrocinado)
        {
            _patrocinados.Add(patrocinado);
        }

        public string GenerateCarteiraPatrocinador()
        {
            return $"{DateTime.Now.Year.ToString()}{Id:00000000}";
        }
    }
}
