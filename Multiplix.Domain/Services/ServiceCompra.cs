
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
    public class ServiceCompra : IServiceCompra
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IParceiroRepository _parceiroRepository;
        private readonly IPatrocinadorRepository  _patrocinadorRepository;

        public ServiceCompra(ICompraRepository compraRepository, IParceiroRepository parceiroRepository, IPatrocinadorRepository patrocinadorRepository)
        {
            _compraRepository = compraRepository;
            _parceiroRepository = parceiroRepository;
            _patrocinadorRepository = patrocinadorRepository;
        }

        public Compra Adicionar(Compra entity)
        {
            return _compraRepository.Adicionar(entity);
        }

        public void Atualizar(Compra entity)
        {
            _compraRepository.Atualizar(entity);
        }

        public IEnumerable<Compra> Buscar(Expression<Func<Compra, bool>> predicado)
        {
            return _compraRepository.Buscar(predicado);
        }

        public Compra BuscarEntidade(Expression<Func<Compra, bool>> predicado)
        {
            return _compraRepository.BuscarEntidade(predicado);
        }

        public Compra ObterPorId(int id)
        {
            return _compraRepository.ObterPorId(id);
        }

        public IEnumerable<Compra> ObterTodos()
        {
            return _compraRepository.ObterTodos();
        }

        public IEnumerable<Compra> ObterTodosPaginado(int skip, int take)
        {
            return _compraRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Compra entity)
        {
            _compraRepository.Remover(entity);
        }

        public ValidationResult Salvar(CompraDTO compraDTO)
        {
            Compra compra;
            Parceiro parceiro = _parceiroRepository.ObterPorId(compraDTO.ParceiroId);

            if (compraDTO.CompraId == 0)
            {
                compra = new Compra(
                    valor: compraDTO.Valor,
                    data: DateTime.Now,
                    pontos: (parceiro.PontoPorReal * compraDTO.Valor),
                    parceiro: parceiro,
                    associado: _patrocinadorRepository.ObterPorId(compraDTO.AssociadoId)
                    );

                compra.CompraId = 0;
            }
            else
            {
                compra = _compraRepository.ObterPorId(compraDTO.CompraId);

                compra.Valor = compraDTO.Valor;
                compra.Data = DateTime.Now;
                compra.Pontos = compraDTO.Pontos;
                compra.Parceiro = _parceiroRepository.ObterPorId(compraDTO.ParceiroId);
                compra.Associado = _patrocinadorRepository.ObterPorId(compraDTO.AssociadoId);
            }
            
            ValidationResult result = new CompraValidator().Validate(compra);

            if (result.IsValid)
            {
                if (compra.CompraId == 0)
                {
                    _compraRepository.Adicionar(compra);
                }

                else
                {
                    _compraRepository.Atualizar(compra);
                }

            }
            else
            {
                compraDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
