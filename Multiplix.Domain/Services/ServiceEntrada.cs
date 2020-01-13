
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
    public class ServiceEntrada : IServiceEntrada
    {
        private readonly IEntradaRepository _entradaRepository;

        public ServiceEntrada(IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
        }

        public Entrada Adicionar(Entrada entity)
        {
            return _entradaRepository.Adicionar(entity);
        }

        public void Atualizar(Entrada entity)
        {
            _entradaRepository.Atualizar(entity);
        }

        public IEnumerable<Entrada> Buscar(Expression<Func<Entrada, bool>> predicado)
        {
            return _entradaRepository.Buscar(predicado);
        }

        public Entrada BuscarEntidade(Expression<Func<Entrada, bool>> predicado)
        {
            return _entradaRepository.BuscarEntidade(predicado);
        }

        public Entrada ObterPorId(int id)
        {
            return _entradaRepository.ObterPorId(id);
        }

        public IEnumerable<Entrada> ObterTodos()
        {
            return _entradaRepository.ObterTodos();
        }

        public IEnumerable<Entrada> ObterTodosPaginado(int skip, int take)
        {
            return _entradaRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Entrada entity)
        {
            _entradaRepository.Remover(entity);
        }       
    }
}
