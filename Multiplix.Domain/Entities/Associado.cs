using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private IList<Bonus> _bonus = new List<Bonus>();

        //Um associado gera várias entrada
        private IList<Entrada> _entradas = new List<Entrada>();

        /// <summary>
        /// Um associado pode fazer várias compras em um associado
        /// </summary>
        private IList<Compra> _compras = new List<Compra>();

        public Associado(Usuario usuario, int? patrocinadorId, string rua, string numero, string cep,  Cidade cidade, 
            string bairro, string complemento, string estado, DateTime nascimento,
            string sexo, string cpf, string emailAlternativo, Banco banco, int tipoConta, string agengia, string conta, int nivel,
            PlanoAssinatura planoAssinatura)
        {
            Usuario = usuario;
            PatrocinadorId = patrocinadorId;
            IdCarteira = $"{DateTime.Now.Year.ToString()}{Id:00000000}";
            _patrocinados = new List<Associado>();
            Rua = rua;
            Cidade = cidade;          
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
            Nivel = nivel;
            PlanoAssinatura = planoAssinatura;
        }

        public Associado()
        {
            _patrocinados = new List<Associado>();
        }

        public int Id { get; set; }
        public int Nivel { get; set; }
        public string IdCarteira { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Cidade Cidade { get; set; }
        public int? CidadeId { get; set; }
        public int UsuarioId { get; set; }
        public virtual PlanoAssinatura PlanoAssinatura { get; set; }
        public int PlanoAssinaturaId { get; set; }
        public int? PatrocinadorId { get; set; }       
        public string Sexo { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }

        public string EmailAlternativo { get; set; }

        public virtual Banco Banco { get; set; }
        public int? BancoId { get; set; }
        public int TipoConta { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }


        public String Rua { get;  set; }      
        public String CEP { get; set; }
        public String Numero { get; set; }
        public String Bairro { get; set; }
        public String Complemento { get; set; }


        public virtual ICollection<Associado> Patrocinados { get => _patrocinados; set { } }
        public virtual ICollection<Compra> Compras { get => _compras; set { } }
        public virtual ICollection<Bonus> Bonus { get => _bonus; set { } }
        public virtual ICollection<Entrada> Entradas { get => _entradas; set { } }

        /// <summary>
        /// Adiciona o associado patrocinado ao associado patrocinador que fez o convite
        /// </summary>
        /// <param name="patrocinado"></param>
        public void AddPatrocinado(Associado patrocinado)
        {
            _patrocinados.Add(patrocinado);
        }

        public void AddCompra(Compra compra)
        {
            _compras.Add(compra);
        }

        public void AddBonus(Bonus bonus)
        {
            _bonus.Add(bonus);
        }

        public void AddEntrada(Entrada entrada)
        {
            _entradas.Add(entrada);
        }

        public string GenerateCarteiraPatrocinador()
        {
            return $"{DateTime.Now.Year.ToString()}{Id:00000000}";
        }

        public string GenerateCodeInvite => Convert.ToBase64String(Encoding.UTF8.GetBytes(IdCarteira));

        public override string ToString()
        {
            return $"{Usuario.Nome} - {IdCarteira}";
        }
    }
}
