using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceEntrada : IServiceBase<Entrada>
    {
        ValidationResult Salvar(EntradaDTO entradaDTO);

        void GerarBonus(int associadoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="associadoDonoId">Dono do bonus</param>
        /// <param name="associadoGeradorId">Quem está gerando o bonus ao se cadastrar</param>
        /// <param name="valor">valor do bonus gerado</param>
        void SalvarBonus(int? associadoDonoId, int associadoGeradorId, float valor);
    }
}
