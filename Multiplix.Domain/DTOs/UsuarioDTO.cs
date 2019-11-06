using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string IdCateira { get; set; }

        //Patrocinador
        public int PatrocinadorId { get; set; }
        public string PatrocinadorNome { get; set; }

        //Associado
        public int AssociadoId { get; set; }
       

        public int Nivel { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public string Sexo { get; set; }

        public string UF { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; }

        public bool Liberado { get; set; }

        public String Rua { get;  set; }
        public String Cidade { get;  set; }
        public String Estado { get;  set; }
        public String CEP { get;  set; }
        public String Numero { get;  set; }

        public int BancoId { get; set; }
        public string BancoNome { get; set; }
        public int TipoConta { get; set; }

        public DateTime Nascimento { get; set; }
        public string CPF { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string EmailAlternativo { get; set; }

        public string Agencia { get; set; }
        public string Conta { get; set; }

        //Parceiro
        public string HorarioFuncionamento { get; set; }      
        public int PontoPorReal { get; set; }
        public int ParceiroId { get; set; }
        public string CNPJ { get; set; }
        public IList<ProdutoDTO> Produtos { get; set; } = new List<ProdutoDTO>();

       

        //Ramo de Atividade
        public string RamoAtividadeNome { get; set; }
        public int RamoAtividadeId { get; set; }

        //Plano de assinatura
        public int PlanoAssinaturaId { get; set; }
        public string PlanoAssinaturaNome { get; set; }
        public int TipoUsuario { get; set; }

        public IList<GrupoDTO> Grupos { get; set; } = new List<GrupoDTO>();
        

        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();

        // permissões
        public int? SuperUser { get; set; }
        public string GruposIDs { get; set; } // 1,2,100,...
        public List<PermissaoDTO> Permissoes { get; set; } = new List<PermissaoDTO>();
        public string PermissoesIDs { get; set; } // 1,2,100,...

    }
}
