using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Enums;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Multiplix.Domain.Services
{
    public class ServiceParceiro : IServiceParceiro
    {
        private const int PatrocinadorRaiz = 1;
        private readonly IParceiroRepository _parceiroRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRamoAtividadeRepository _ramoAtividade;
        private readonly ICidadeRepository _cidadeRepository;

        public ServiceParceiro(IParceiroRepository parceiroRepository, 
            IUsuarioRepository usuarioRepository, IRamoAtividadeRepository ramoAtividade, ICidadeRepository cidadeRepository)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _ramoAtividade = ramoAtividade;
            _cidadeRepository = cidadeRepository;
        }

        public Parceiro Adicionar(Parceiro entity)
        {
            return _parceiroRepository.Adicionar(entity);
        }

        public void Atualizar(Parceiro entity)
        {
            _parceiroRepository.Atualizar(entity);
        }

        public IEnumerable<Parceiro> Buscar(Expression<Func<Parceiro, bool>> predicado)
        {
            return _parceiroRepository.Buscar(predicado);
        }

        public Parceiro BuscarEntidade(Expression<Func<Parceiro, bool>> predicado)
        {
            return _parceiroRepository.BuscarEntidade(predicado);
        }

        public List<ListaProdutoParceiroDTO> ListaProdutosParceiroDTO(string pesquisa, int cidadeId)
        {
            var todosParceiros = _parceiroRepository.ObterTodos();
            List<ListaProdutoParceiroDTO> listaProdutosParceiroDTO = new List<ListaProdutoParceiroDTO>();

            foreach (var Parceiro in todosParceiros)
            {
                foreach (var produtoParceiro in Parceiro.ParceiroProdutos.Where(x => x.Produto.Descricao.ToUpper().Contains(pesquisa.ToUpper()) && x.Parceiro.Cidade.CidadeId == cidadeId))
                {
                    var produto = new ListaProdutoParceiroDTO(
                        produtoParceiro.Parceiro.ParceiroId,
                        produtoParceiro.Parceiro.Usuario.Nome,
                        produtoParceiro.Parceiro.Usuario.Celular,
                        produtoParceiro.Parceiro.Rua,
                        produtoParceiro.Produto.Descricao,
                        produtoParceiro.ValorProduto,
                        produtoParceiro.PontosPorRealProduto
                    );

                    listaProdutosParceiroDTO.Add(produto);
                }

            }

            return listaProdutosParceiroDTO;
        }

        public List<ListaProdutoParceiroDTO> ListaProdutosParceiroDTO(int cidadeId)
        {
            var todosParceiros = _parceiroRepository.ObterTodos().Where(x => x.Cidade.CidadeId == cidadeId);
            List<ListaProdutoParceiroDTO> listaProdutosParceiroDTO = new List<ListaProdutoParceiroDTO>();

            foreach (var Parceiro in todosParceiros)
            {
                foreach (var produtoParceiro in Parceiro.ParceiroProdutos)
                {
                    var produto = new ListaProdutoParceiroDTO(
                        produtoParceiro.Parceiro.ParceiroId,
                        produtoParceiro.Parceiro.Usuario.Nome,
                        produtoParceiro.Parceiro.Usuario.Celular,
                        produtoParceiro.Parceiro.Rua,
                        produtoParceiro.Produto.Descricao,
                        produtoParceiro.ValorProduto,
                        produtoParceiro.PontosPorRealProduto
                    );

                    listaProdutosParceiroDTO.Add(produto);
                }

            }

            return listaProdutosParceiroDTO;
        }

        public List<ListaRepasseParceiroDTO> ListaRepasseParceiroDTO(DateTime dataInicio, DateTime dataFim)
        {
            var todosParceiros = _parceiroRepository.ObterTodos();
            List<ListaRepasseParceiroDTO> listaRepasseParceiroDTO = new List<ListaRepasseParceiroDTO>();

            foreach (var parceiro in todosParceiros)
            {
                int qtdVendas = parceiro.Compras.Where(x => x.Data >= dataInicio && x.Data <= dataFim).Count();

                var repasseParceiro = new ListaRepasseParceiroDTO(
                    parceiroId: parceiro.ParceiroId,
                    parceiro: parceiro.Usuario.Nome,
                    valorRepasse:(Decimal)parceiro.Compras.Where(x => x.Data >= dataInicio && x.Data <= dataFim).Sum(x => x.CompraItems.Sum(y => y.ValorRepasse)),
                    numerovendas: qtdVendas
                    );

                if (qtdVendas > 0)
                {
                    listaRepasseParceiroDTO.Add(repasseParceiro);
                }
                
            }

            return listaRepasseParceiroDTO;
        }

        public List<ListaRepasseParceiroDTO> ListaRepasseParceiroDTO(DateTime dataInicio, DateTime dataFim, int parceiroId)
        {
            var todosParceiros = _parceiroRepository.ObterTodos();
            List<ListaRepasseParceiroDTO> listaRepasseParceiroDTO = new List<ListaRepasseParceiroDTO>();

            var parceiro = _parceiroRepository.ObterPorId(parceiroId);
           
                int qtdVendas = parceiro.Compras.Where(x => x.Data >= dataInicio && x.Data <= dataFim).Count();

                var repasseParceiro = new ListaRepasseParceiroDTO(
                    parceiroId: parceiro.ParceiroId,
                    parceiro: parceiro.Usuario.Nome,
                    valorRepasse: (Decimal)parceiro.Compras.Where(x => x.Data >= dataInicio && x.Data <= dataFim).Sum(x => x.CompraItems.Sum(y => y.ValorRepasse)),
                    numerovendas: qtdVendas
                    );

                if (qtdVendas > 0)
                {
                    listaRepasseParceiroDTO.Add(repasseParceiro);
                }

           

            return listaRepasseParceiroDTO;
        }

        public Parceiro ObterPorId(int id)
        {
            return _parceiroRepository.ObterPorId(id);
        }

        public IEnumerable<Parceiro> ObterTodos()
        {
            return _parceiroRepository.ObterTodos();
        }

        public IEnumerable<Parceiro> ObterTodosPaginado(int skip, int take)
        {
            return _parceiroRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Parceiro entity)
        {
            _parceiroRepository.Remover(entity);
        }

       /// <summary>
       /// Parceiros são as empresas filiadas a multiplix que vendem produtos para os associados
       /// cada produto valem pontos, que quando convertidos podem ser trocados por dinheiro
       /// </summary>
       /// <param name="usuarioDTO"></param>
       /// <returns></returns>
        public ValidationResult SalvarParceiro(UsuarioDTO usuarioDTO)
        {
            // usuário
            Usuario usuario;
            Parceiro parceiro;
            var atualizando = false;
           
            if (usuarioDTO.UsuarioId == 0)
            {
                atualizando = false;
                //cria o usuário do patrocinador
                usuario = new Usuario(
                    login: usuarioDTO.Login,
                    senha: usuarioDTO.Senha,
                    nome: usuarioDTO.Nome,
                    celular: usuarioDTO.Celular,
                    email: usuarioDTO.Email,
                    liberado: usuarioDTO.Liberado,
                    tipoUsuario: (int)ETipoUsuario.PARCEIRO
                );               

                //associa o usuário ao parceiro
                parceiro = new Parceiro(
                    usuario: usuario,
                    horarioFuncionamento: usuarioDTO.HorarioFuncionamento,
                    rua: usuarioDTO.Rua,
                    numero: usuarioDTO.Numero,
                    cep: usuarioDTO.CEP,
                    cidade: _cidadeRepository.ObterPorId(usuarioDTO.CidadeId),
                    bairro: usuarioDTO.Bairro,
                    complemento: usuarioDTO.Complemento,                   
                    ramo: _ramoAtividade.ObterPorId(usuarioDTO.RamoAtividadeId),                                   
                    cnpj: usuarioDTO.CNPJ,
                    responsavel: usuarioDTO.Responsavel                    
                    ) ;

                UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                usuarioGrupo.UsuarioId = parceiro.Usuario.UsuarioId;
                usuarioGrupo.GrupoId = 2;
                parceiro.Usuario.AddUsuarioGrupo(usuarioGrupo);

                parceiro.ParceiroId = 0;
            }
            else
            {
                atualizando = true;

                parceiro = _parceiroRepository.Buscar(x => x.Usuario.UsuarioId == usuarioDTO.UsuarioId).FirstOrDefault();

                parceiro.Usuario.Login = usuarioDTO.Login;

                //Na edição só atualiza a senha se for digitada alguma
                if (!String.IsNullOrEmpty(usuarioDTO.Senha))
                {
                    parceiro.Usuario.Senha = usuarioDTO.Senha;
                }
               
                parceiro.Usuario.Nome = usuarioDTO.Nome;
                parceiro.Usuario.Celular = usuarioDTO.Celular;
                parceiro.Usuario.Email = usuarioDTO.Email;
                parceiro.Usuario.Liberado = usuarioDTO.Liberado;

                parceiro.Rua = usuarioDTO.Rua;
                parceiro.Numero = usuarioDTO.Numero;
                parceiro.CEP = usuarioDTO.CEP;
                parceiro.Cidade = _cidadeRepository.ObterPorId(usuarioDTO.CidadeId);
                parceiro.Bairro = usuarioDTO.Bairro;
                parceiro.Complemento = usuarioDTO.Complemento;               
                parceiro.Ramo = _ramoAtividade.ObterPorId(usuarioDTO.RamoAtividadeId);                
                parceiro.HorarioFuncionamento = usuarioDTO.HorarioFuncionamento;
                parceiro.CNPJ = usuarioDTO.CNPJ;
                parceiro.Responsavel = usuarioDTO.Responsavel;
           
            }

            #region  grupos do usuário do parceiro
         
            //if (usuarioDTO.ParceiroId > 0)
            //    _usuarioRepository.DeleteUsuarioGrupos(parceiro.Usuario.UsuarioId);

            //if (usuarioDTO.Grupos.Count > 0)
            //{
            //    foreach (var grupoDTO in usuarioDTO.Grupos)
            //    {
            //        UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            //        usuarioGrupo.UsuarioId = parceiro.Usuario.UsuarioId;
            //        usuarioGrupo.GrupoId = grupoDTO.GrupoId;

            //        // adiciona o grupo ao usuário via patrocinador
            //        parceiro.Usuario.AddUsuarioGrupo(usuarioGrupo);
            //    }
            //}
            #endregion

            #region produtos do parceiro
            if (usuarioDTO.UsuarioId > 0)
                _parceiroRepository.DeleteProdutosParceiro(parceiro.ParceiroId);

            if (usuarioDTO.Produtos.Count > 0)
            {
                foreach (var produto in usuarioDTO.Produtos)
                {
                    ParceiroProduto parceiroProduto = new ParceiroProduto();
                    parceiroProduto.ParceiroId = parceiro.ParceiroId;
                    parceiroProduto.ProdutoId = produto.ProdutoId;
                    parceiroProduto.PontosPorRealProduto = produto.PontosPorRealProduto;
                    parceiroProduto.ValorProduto = produto.ValorProduto;
                    parceiroProduto.PercentualRepasseAtual = produto.PercentualRepasse;

                    // adiciona o produto ao parceiro
                    parceiro.AddProdutoParceiro(parceiroProduto);
                }
            }

            #endregion

            ValidationResult result = new UsuarioValidator(_usuarioRepository, atualizando).Validate(parceiro.Usuario);

            //se estou inserindo a senha é obrigatória
            if ((!atualizando) && (string.IsNullOrEmpty(parceiro.Usuario.Senha)))
            {
                result.Errors.Add(new ValidationFailure("Senha", "A Senha é obrigatória."));
            }           

            if (parceiro.Cidade == null)
            {
                result.Errors.Add(new ValidationFailure("Cidade", "Campo obrigatório."));
            }

            if (result.IsValid)
            {
                if (parceiro.ParceiroId == 0)
                {
                    _parceiroRepository.Adicionar(parceiro);                  
                }

                else
                {
                    _parceiroRepository.Atualizar(parceiro);
                }
                   
            }
            else
            {
                usuarioDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
