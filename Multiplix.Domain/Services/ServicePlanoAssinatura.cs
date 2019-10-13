
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
    public class ServicePlanoAssinatura : IServicePlanoAssinatura
    {
        private readonly IPlanoAssinaturaRepository _planoAssinaturaRepository;

        public ServicePlanoAssinatura(IPlanoAssinaturaRepository planoAssinaturaRepository)
        {
            _planoAssinaturaRepository = planoAssinaturaRepository;
        }

        public PlanoAssinatura Adicionar(PlanoAssinatura entity)
        {
            return _planoAssinaturaRepository.Adicionar(entity);
        }

        public void Atualizar(PlanoAssinatura entity)
        {
            _planoAssinaturaRepository.Atualizar(entity);
        }

        public IEnumerable<PlanoAssinatura> Buscar(Expression<Func<PlanoAssinatura, bool>> predicado)
        {
            return _planoAssinaturaRepository.Buscar(predicado);
        }

        public PlanoAssinatura BuscarEntidade(Expression<Func<PlanoAssinatura, bool>> predicado)
        {
            return _planoAssinaturaRepository.BuscarEntidade(predicado);
        }

        public PlanoAssinatura ObterPorId(int id)
        {
            return _planoAssinaturaRepository.ObterPorId(id);
        }

        public IEnumerable<PlanoAssinatura> ObterTodos()
        {
            return _planoAssinaturaRepository.ObterTodos();
        }

        public IEnumerable<PlanoAssinatura> ObterTodosPaginado(int skip, int take)
        {
            return _planoAssinaturaRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(PlanoAssinatura entity)
        {
            _planoAssinaturaRepository.Remover(entity);
        }

        public ValidationResult Salvar(PlanoAssinaturaDTO planoAssinaturaDTO)
        {
            // plano de assinatura
            PlanoAssinatura planoAssinatura;

            if (planoAssinaturaDTO.PlanoAssinaturaId == 0)
            {
                planoAssinatura = new PlanoAssinatura(                   
                   valor: planoAssinaturaDTO.Valor,
                   descricao: planoAssinaturaDTO.Descricao
                );

                planoAssinatura.PlanoAssinaturaId = 0;
            }
            else
            {
                planoAssinatura = _planoAssinaturaRepository.ObterPorId(planoAssinaturaDTO.PlanoAssinaturaId);
                planoAssinatura.Descricao = planoAssinaturaDTO.Descricao;
                planoAssinatura.Valor = planoAssinaturaDTO.Valor;
            }

            ValidationResult result = new PlanoAssinaturaValidator().Validate(planoAssinatura);

            if (result.IsValid)
            {
                if (planoAssinatura.PlanoAssinaturaId == 0)
                    _planoAssinaturaRepository.Adicionar(planoAssinatura);
                else
                    _planoAssinaturaRepository.Atualizar(planoAssinatura);
            }
            else
            {
                planoAssinaturaDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
