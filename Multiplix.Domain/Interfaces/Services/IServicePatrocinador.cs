using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServicePatrocinador : IServiceBase<Associado>
    {
       
        ValidationResult SalvarAssociadoSemConvite(UsuarioDTO usuarioDTO);

        ValidationResult SalvarAssociadoComConvite(UsuarioDTO usuarioDTO);

        /// <summary>
        /// Método responsável em retornar toda a rede do associado, ou seja, todos os seus filhos diretos e indiretos
        /// </summary>
        /// <param name="associadoId">Id do associado patrocinador</param>
        /// <returns>rede de associados que são patrocinados</returns>
        List<Associado> GetRedeAssociado(int associadoId);

        /// <summary>
        /// Método responsável em retornara percentagem atual do associado baseado no total de pontos atual dele
        /// </summary>
        /// <param name="totalGeralPontos">Soma dos pontos individuais mais pontos da rede do usuário</param>
        /// <returns></returns>
        int GetPercentagem(float totalGeralPontos);

        /// <summary>
        /// Método responsável em retornar os pontos individuais do associado no mes, ou seja, todos os pontos conquistados 
        /// com suas compras diretas
        /// </summary>
        /// <param name="mes">mês das compras realizadas</param>
        /// <returns></returns>
        float GetPontosIndividuaisPorMes(int mes, int associadoId);

        /// <summary>
        /// Método responsável em retornar todos os pontos da rede do associado no mês
        /// </summary>
        /// <param name="mes">mês escolhido</param>
        /// <param name="associadoId">id do associado</param>
        /// <returns></returns>
        float GetPontosRedePorMes(int mes, int associadoId);

        /// <summary>
        /// Método responsável em gerar os ganhos individuais do associado, baseado na soma de pontos individual dele
        /// </summary>
        /// <param name="mes">mes</param>
        /// <param name="percentagem">Valor da percentagem calculado pelo método GetPercentagem </param>
        /// <returns></returns>
        float GetGanhosIndividual(int mes, int associadoId);

        /// <summary>
        /// Método responsável em gerar os ganhos da rede do associado, beseado nos associados diretos.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="associadoId"></param>
        /// <returns></returns>
        float GetGanhosRede(int mes, int associadoId);

        /// <summary>
        /// Retornar os pontos individuais mais os pontos totais
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="associadoId"></param>
        /// <returns></returns>
        float GetPontosTotal(int mes, int associadoId);

        bool CPFJaExiste(string cpf);
        List<Associado> GetAssociadosAniversariantes(int mesNascimento);
        List<DiasSemComprarDTO> GetDiasSemConsumo(int dias);


    }
}
