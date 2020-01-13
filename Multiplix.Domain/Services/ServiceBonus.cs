
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
    public class ServiceBonus : IServiceBonus
    {
        private readonly IBonusRepository _bonusRepository;

        public ServiceBonus(IBonusRepository bonusRepository)
        {
            _bonusRepository = bonusRepository;
        }

        public Bonus Adicionar(Bonus entity)
        {
            return _bonusRepository.Adicionar(entity);
        }

        public void Atualizar(Bonus entity)
        {
            _bonusRepository.Atualizar(entity);
        }

        public IEnumerable<Bonus> Buscar(Expression<Func<Bonus, bool>> predicado)
        {
            return _bonusRepository.Buscar(predicado);
        }

        public Bonus BuscarEntidade(Expression<Func<Bonus, bool>> predicado)
        {
            return _bonusRepository.BuscarEntidade(predicado);
        }

        public Bonus ObterPorId(int id)
        {
            return _bonusRepository.ObterPorId(id);
        }

        public IEnumerable<Bonus> ObterTodos()
        {
            return _bonusRepository.ObterTodos();
        }

        public IEnumerable<Bonus> ObterTodosPaginado(int skip, int take)
        {
            return _bonusRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Bonus entity)
        {
            _bonusRepository.Remover(entity);
        }       
    }
}
