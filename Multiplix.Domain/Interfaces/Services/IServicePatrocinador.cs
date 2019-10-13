using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using System.Collections.Generic;

namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServicePatrocinador : IServiceBase<Associado>
    {
       
        ValidationResult SalvarAssociadoSemConvite(UsuarioDTO usuarioDTO);

        /// <summary>
        /// Método responsável em retornar toda a rede do associado, ou seja, todos os seus filhos diretos e indiretos
        /// </summary>
        /// <param name="associadoId">Id do associado patrocinador</param>
        /// <returns>rede de associados que são patrocinados</returns>
        List<Associado> GetRedeAssociado(int associadoId);


    }
}
