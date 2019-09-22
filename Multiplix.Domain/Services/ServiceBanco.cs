
using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Multiplix.Domain.Services
{
    public class ServiceBanco : IServiceBanco
    {
        private readonly IBancoRepository _bancoRepository;

        public ServiceBanco(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }

        public Banco Adicionar(Banco entity)
        {
            return _bancoRepository.Adicionar(entity);
        }

        public void Atualizar(Banco entity)
        {
            _bancoRepository.Atualizar(entity);
        }

        public IEnumerable<Banco> Buscar(Expression<Func<Banco, bool>> predicado)
        {
            return _bancoRepository.Buscar(predicado);
        }

        public Banco BuscarEntidade(Expression<Func<Banco, bool>> predicado)
        {
            return _bancoRepository.BuscarEntidade(predicado);
        }

        public Banco ObterPorId(int id)
        {
            return _bancoRepository.ObterPorId(id);
        }

        public IEnumerable<Banco> ObterTodos()
        {
            return _bancoRepository.ObterTodos();
        }

        public IEnumerable<Banco> ObterTodosPaginado(int skip, int take)
        {
            return _bancoRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Banco entity)
        {
            _bancoRepository.Remover(entity);
        }

        public ValidationResult Salvar(BancoDTO bancoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
