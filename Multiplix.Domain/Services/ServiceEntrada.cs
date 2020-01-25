
using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Enums;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BoletoNetCore;
using BoletoNetCore.Pdf.BoletoImpressao;

namespace Multiplix.Domain.Services
{
    
    public class ServiceEntrada : IServiceEntrada
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IPatrocinadorRepository _patrocinadorRepository;
        private readonly IParceiroRepository _parceiroRepository;
        private readonly IBonusRepository _bonusRepository;
                

        public ServiceEntrada(IEntradaRepository entradaRepository, IPatrocinadorRepository patrocinadorRepository, IParceiroRepository parceiroRepository, IBonusRepository bonusRepository)
        {
            _entradaRepository = entradaRepository;
            _patrocinadorRepository = patrocinadorRepository;
            _parceiroRepository = parceiroRepository;
            _bonusRepository = bonusRepository;
        }

        public Entrada Adicionar(Entrada entity)
        {
            BoletoBancarioPdf  boleto = new BoletoBancarioPdf();
            
            BoletoNetCore.Pdf.BoletoNetCorePdfProxy t = new BoletoNetCore.Pdf.BoletoNetCorePdfProxy();
           
            // var boleto2 = new BoletoNetCore.Pdf.

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

        public void GerarBonus(int associadoId)
        {
            int? patrocinadorIdPai1 = null;
            int? patrocinadorIdPai2 = null;
            int? patrocinadorIdPai3 = null;

            for (int i = 1; i <= 3; i++)
            {
                if (i == 1) // gera o primeiro bonus para o pai
                {
                    patrocinadorIdPai1 = _patrocinadorRepository.ObterPorId(associadoId).PatrocinadorId.Value;
                    if (patrocinadorIdPai1 != null && patrocinadorIdPai1 != 1)
                    {
                        SalvarBonus(patrocinadorIdPai1.Value, associadoId, 6);
                    }

                }
                else if (i == 2)
                {
                    patrocinadorIdPai2 = _patrocinadorRepository.ObterPorId(patrocinadorIdPai1.Value).PatrocinadorId.Value;
                    if (patrocinadorIdPai2 != null && patrocinadorIdPai2 != 1)
                    {
                        SalvarBonus(patrocinadorIdPai2.Value, associadoId, 3);
                    }
                }
                else if (i == 3)
                {
                    if (patrocinadorIdPai3 != null && patrocinadorIdPai3 != 1)
                    {
                        patrocinadorIdPai3 = _patrocinadorRepository.ObterPorId(patrocinadorIdPai2.Value).PatrocinadorId.Value;
                        SalvarBonus(patrocinadorIdPai3.Value, associadoId, 1);
                    }
                }
            }
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

        public ValidationResult Salvar(EntradaDTO entradaDTO)
        {
            // entrada
            Entrada entrada;

            if (entradaDTO.EntradaId == 0)
            {
                entrada = new Entrada(
                    descricao: entradaDTO.Descricao,
                    data: DateTime.Now,
                    associado: _patrocinadorRepository.ObterPorId(entradaDTO.AssociadoId),
                    status: entradaDTO.Status,
                    valor: entradaDTO.Valor,
                    dataVencimento: entradaDTO.DataVencimento,
                    tipoEntrada: (int)ETipoEntrada.MANUAL
                );

                entrada.EntradaId = 0;
            }
            else
            {
                entrada = _entradaRepository.ObterPorId(entradaDTO.EntradaId);
                entrada.Descricao = entradaDTO.Descricao;
                entrada.Data = DateTime.Now;
                entrada.Associado = _patrocinadorRepository.ObterPorId(entradaDTO.AssociadoId);
                entrada.Status = entradaDTO.Status;
                entrada.Valor = entradaDTO.Valor;
                entradaDTO.DataVencimento = entradaDTO.DataVencimento;
            }

            ValidationResult result = new EntradaValidator().Validate(entrada);

            if (result.IsValid)
            {
                if (entrada.EntradaId == 0)
                    _entradaRepository.Adicionar(entrada);
                else
                    _entradaRepository.Atualizar(entrada);

                    if ((entrada.TipoEntrada == (int)ETipoEntrada.ADESAO) && (entradaDTO.Status ==(int)EStatusMovimentacao.PAGO))
                    {
                        GerarBonus(entrada.Associado.Id);
                        
                       //Libera o acesso
                       entrada.Associado.Usuario.Liberado = true;
                       _entradaRepository.Atualizar(entrada);
                    }
            }
            else
            {
                entradaDTO.ValidationErrors = result.Errors;
            }

            return result;
        }

        public void SalvarBonus(int? associadoDonoId, int associadoGeradorId, float valor)
        {
            Bonus bonus = new Bonus(
               valor: valor,
               dataCadastro: DateTime.Now,
               dono: _patrocinadorRepository.ObterPorId(associadoDonoId.Value),
               gerador: _patrocinadorRepository.ObterPorId(associadoGeradorId)
               );

            _bonusRepository.Adicionar(bonus);

        }
    }
}
