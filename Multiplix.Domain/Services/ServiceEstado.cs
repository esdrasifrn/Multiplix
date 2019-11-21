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
    public class ServiceEstado : IServiceEstado
    {
        private readonly IEstadoRepository  _estadoRepository;


        public ServiceEstado(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;         
        }

        public Estado Adicionar(Estado entity)
        {
            return _estadoRepository.Adicionar(entity);
        }

        public void Atualizar(Estado entity)
        {
            _estadoRepository.Atualizar(entity);
        }      

        public IEnumerable<Estado> Buscar(Expression<Func<Estado, bool>> predicado)
        {
            return _estadoRepository.Buscar(predicado);
        }

        public Estado BuscarEntidade(Expression<Func<Estado, bool>> predicado)
        {
            return _estadoRepository.BuscarEntidade(predicado);
        }

        public Estado ObterPorId(int id)
        {
            return _estadoRepository.ObterPorId(id);
        }

        public IEnumerable<Estado> ObterTodos()
        {
            return _estadoRepository.ObterTodos();
        }

        public IEnumerable<Estado> ObterTodosPaginado(int skip, int take)
        {
            return _estadoRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Estado entity)
        {
            _estadoRepository.Remover(entity);
        }
    }
}
