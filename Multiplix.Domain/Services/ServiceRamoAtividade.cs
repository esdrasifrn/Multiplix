
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
    public class ServiceRamoAtividade : IServiceRamoAtividade
    {
        private readonly IRamoAtividadeRepository _ramoAtividadeRepository;

        public ServiceRamoAtividade(IRamoAtividadeRepository ramoAtividadeRepository)
        {
            _ramoAtividadeRepository = ramoAtividadeRepository;
        }

        public RamoAtividade Adicionar(RamoAtividade entity)
        {
            return _ramoAtividadeRepository.Adicionar(entity);
        }

        public void Atualizar(RamoAtividade entity)
        {
            _ramoAtividadeRepository.Atualizar(entity);
        }

        public IEnumerable<RamoAtividade> Buscar(Expression<Func<RamoAtividade, bool>> predicado)
        {
            return _ramoAtividadeRepository.Buscar(predicado);
        }

        public RamoAtividade BuscarEntidade(Expression<Func<RamoAtividade, bool>> predicado)
        {
            return _ramoAtividadeRepository.BuscarEntidade(predicado);
        }

        public RamoAtividade ObterPorId(int id)
        {
            return _ramoAtividadeRepository.ObterPorId(id);
        }

        public IEnumerable<RamoAtividade> ObterTodos()
        {
            return _ramoAtividadeRepository.ObterTodos();
        }

        public IEnumerable<RamoAtividade> ObterTodosPaginado(int skip, int take)
        {
            return _ramoAtividadeRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(RamoAtividade entity)
        {
            _ramoAtividadeRepository.Remover(entity);
        }

        public ValidationResult Salvar(RamoAtividadeDTO ramoAtividadeDTO)
        {
            throw new NotImplementedException();
        }
    }
}
