using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceBanco : IServiceBase<Banco>
    {
        ValidationResult Salvar(BancoDTO bancoDTO);     
    }
}
