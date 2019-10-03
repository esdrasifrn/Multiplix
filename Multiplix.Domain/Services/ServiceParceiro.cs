using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
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

        public ServiceParceiro(IParceiroRepository parceiroRepository, 
            IUsuarioRepository usuarioRepository, IRamoAtividadeRepository ramoAtividade)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _ramoAtividade = ramoAtividade;
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
           
            if (usuarioDTO.ParceiroId == 0)
            { 

                //cria o usuário do patrocinador
                usuario = new Usuario(
                    login: usuarioDTO.Login,
                    senha: usuarioDTO.Senha,
                    nome: usuarioDTO.Nome,
                    celular: usuarioDTO.Celular,
                    email: usuarioDTO.Email,
                    liberado: usuarioDTO.Liberado
                );               

                //associa o usuário ao parceiro
                parceiro = new Parceiro(
                    usuario: usuario,
                    horarioFuncionamento: usuarioDTO.HorarioFuncionamento,
                    rua: usuarioDTO.Rua,
                    numero: usuarioDTO.Numero,
                    cep: usuarioDTO.CEP,
                    cidade: usuarioDTO.Cidade,
                    bairro: usuarioDTO.Bairro,
                    complemento: usuarioDTO.Complemento,
                    estado: usuarioDTO.Estado,
                    ramo: _ramoAtividade.ObterPorId(usuarioDTO.RamoAtividadeId),
                    pontoPorReal: usuarioDTO.PontoPorReal,                   
                    cnpj: usuarioDTO.CNPJ
                    
                    ) ; 

                parceiro.ParceiroId = 0;
            }
            else
            {
                parceiro = _parceiroRepository.ObterPorId(usuarioDTO.ParceiroId);

                parceiro.Usuario.Login = usuarioDTO.Login;
                parceiro.Usuario.Senha = usuarioDTO.Senha;
                parceiro.Usuario.Nome = usuarioDTO.Nome;
                parceiro.Usuario.Celular = usuarioDTO.Celular;
                parceiro.Usuario.Email = usuarioDTO.Email;
                parceiro.Usuario.Liberado = usuarioDTO.Liberado;

                parceiro.Rua = usuarioDTO.Rua;
                parceiro.Numero = usuarioDTO.Numero;
                parceiro.CEP = usuarioDTO.CEP;
                parceiro.Cidade = usuarioDTO.Cidade;
                parceiro.Bairro = usuarioDTO.Bairro;
                parceiro.Complemento = usuarioDTO.Complemento;
                parceiro.Estado = usuarioDTO.Estado;
                parceiro.Ramo = _ramoAtividade.ObterPorId(usuarioDTO.RamoAtividadeId);
                parceiro.PontoPorReal = usuarioDTO.PontoPorReal;
                parceiro.HorarioFuncionamento = usuarioDTO.HorarioFuncionamento;
                parceiro.CNPJ = usuarioDTO.CNPJ;
           
            }
                       
            #region  grupos do usuário do parceiro
            if (usuarioDTO.ParceiroId > 0)
                _usuarioRepository.DeleteUsuarioGrupos(parceiro.Usuario.UsuarioId);

            if (usuarioDTO.Grupos.Count > 0)
            {
                foreach (var grupoDTO in usuarioDTO.Grupos)
                {
                    UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                    usuarioGrupo.UsuarioId = parceiro.Usuario.UsuarioId;
                    usuarioGrupo.GrupoId = grupoDTO.GrupoId;

                    // adiciona o grupo ao usuário via patrocinador
                    parceiro.Usuario.AddUsuarioGrupo(usuarioGrupo);
                }
            }
            #endregion
                        
            #region produtos do parceiro
            if (usuarioDTO.ParceiroId > 0)
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

                    // adiciona o produto ao parceiro
                    parceiro.AddProdutoParceiro(parceiroProduto);
                }
            }

            #endregion

            ValidationResult result = new UsuarioValidator().Validate(parceiro.Usuario);           

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
