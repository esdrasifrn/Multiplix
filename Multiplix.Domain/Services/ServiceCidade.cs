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
    public class ServiceCidade : IServiceCidade
    {
        private readonly ICidadeRepository  _cidadeRepository;
        private readonly IEstadoRepository _estadoRepository;


        public ServiceCidade(ICidadeRepository cidadeRepository, IEstadoRepository estadoRepository)
        {
            _cidadeRepository = cidadeRepository;
            _estadoRepository = estadoRepository;
        }

        public Cidade Adicionar(Cidade entity)
        {
            return _cidadeRepository.Adicionar(entity);
        }

        public void Atualizar(Cidade entity)
        {
            _cidadeRepository.Atualizar(entity);
        }      

        public IEnumerable<Cidade> Buscar(Expression<Func<Cidade, bool>> predicado)
        {
            return _cidadeRepository.Buscar(predicado);
        }

        public Cidade BuscarEntidade(Expression<Func<Cidade, bool>> predicado)
        {
            return _cidadeRepository.BuscarEntidade(predicado);
        }

        public Cidade ObterPorId(int id)
        {
            return _cidadeRepository.ObterPorId(id);
        }

        public IEnumerable<Cidade> ObterTodos()
        {
            return _cidadeRepository.ObterTodos();
        }

        public IEnumerable<Cidade> ObterTodosPaginado(int skip, int take)
        {
            return _cidadeRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Cidade entity)
        {
            _cidadeRepository.Remover(entity);
        }

        public ValidationResult Salvar(CidadeDTO cidadeDTO)
        {
            // cidade
            Cidade cidade;

            if (cidadeDTO.CidadeId == 0)
            {
                cidade = new Cidade(
                   descricao: cidadeDTO.Descricao,
                   estado: _estadoRepository.ObterPorId(cidadeDTO.EstadoId)
                );

                cidade.CidadeId = 0;
            }
            else
            {
                cidade = _cidadeRepository.ObterPorId(cidadeDTO.CidadeId);
                cidade.Descricao = cidadeDTO.Descricao;
                cidade.Estado = _estadoRepository.ObterPorId(cidadeDTO.EstadoId);
            }         

            ValidationResult result = new CidadeValidator().Validate(cidade);

            if (result.IsValid)
            {
                if (cidade.CidadeId == 0)
                    _cidadeRepository.Adicionar(cidade);
                else
                    _cidadeRepository.Atualizar(cidade);
            }
            else
            {
                cidadeDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
