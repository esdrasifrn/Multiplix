using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServicePlanoAssinatura : IServiceBase<PlanoAssinatura>
    {
        ValidationResult Salvar(PlanoAssinaturaDTO planoAssinaturaDTO);     
    }
}
