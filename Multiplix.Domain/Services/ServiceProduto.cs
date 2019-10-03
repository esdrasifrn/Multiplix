using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QuizCorp.Domain.Services
{
    public class ServiceProduto : IServiceProduto
    {
        private readonly IProdutoRepository  _produtoRepository;
        private readonly IParceiroRepository _parceiroRepository;

        public ServiceProduto(IProdutoRepository produtoRepository, IParceiroRepository parceiroRepository)
        {
            _produtoRepository = produtoRepository;
            _parceiroRepository = parceiroRepository;
        }

        public Produto Adicionar(Produto entity)
        {
            return _produtoRepository.Adicionar(entity);
        }

        public void Atualizar(Produto entity)
        {
            _produtoRepository.Atualizar(entity);
        }      

        public IEnumerable<Produto> Buscar(Expression<Func<Produto, bool>> predicado)
        {
            return _produtoRepository.Buscar(predicado);
        }

        public Produto BuscarEntidade(Expression<Func<Produto, bool>> predicado)
        {
            return _produtoRepository.BuscarEntidade(predicado);
        }

        public Produto ObterPorId(int id)
        {
            return _produtoRepository.ObterPorId(id);
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return _produtoRepository.ObterTodos();
        }

        public IEnumerable<Produto> ObterTodosPaginado(int skip, int take)
        {
            return _produtoRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Produto entity)
        {
            _produtoRepository.Remover(entity);
        }

        public ValidationResult Salvar(ProdutoDTO produtoDTO)
        {
            // produto
            Produto produto;

            if (produtoDTO.ProdutoId == 0)
            {
                produto = new Produto(
                   descricao: produtoDTO.Descricao
                );

                produto.ProdutoId = 0;
            }
            else
            {
                produto = _produtoRepository.ObterPorId(produtoDTO.ProdutoId);
                produto.Descricao = produtoDTO.Descricao;              
            }         

            ValidationResult result = new ProdutoValidator().Validate(produto);

            if (result.IsValid)
            {
                if (produto.ProdutoId == 0)
                    _produtoRepository.Adicionar(produto);
                else
                    _produtoRepository.Atualizar(produto);
            }
            else
            {
                produtoDTO.ValidationErrors = result.Errors;
            }

            return result;
        }

        public ValidationResult SalvarProdutoParceiro(ProdutoParceiroDTO produtoParceiroDTO)
        {
            //Recuperando o parceiro que será adicionado os produtos
            Parceiro parceiro;
            parceiro = _parceiroRepository.ObterPorId(produtoParceiroDTO.ParceiroId);

            // produtos do parceiro
            //if (produtoParceiroDTO.ParceiroId > 0)
            //    _usuarioRepository.DeleteUsuarioGrupos(parceiro.Usuario.UsuarioId);

            if (produtoParceiroDTO.Produtos.Count > 0)
            {
                foreach (var produto in produtoParceiroDTO.Produtos)
                {
                    ParceiroProduto parceiroProduto = new ParceiroProduto();
                    parceiroProduto.ParceiroId = parceiro.ParceiroId;
                    parceiroProduto.ProdutoId = produto.ProdutoId;

                    // adiciona o produto ao parceiro
                    parceiro.AddProdutoParceiro(parceiroProduto);
                }
            }

            ValidationResult result = new UsuarioValidator().Validate(parceiro.Usuario);

            if (result.IsValid)
            {
                _parceiroRepository.Atualizar(parceiro);                
            }
            //else
            //{
            //    ProdutoParceiroDTO.ValidationErrors = result.Errors;
            //}

            return result;
        }
    }
}
