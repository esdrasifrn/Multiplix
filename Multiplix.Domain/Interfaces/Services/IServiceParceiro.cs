using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceParceiro : IServiceBase<Parceiro>
    {
       
        ValidationResult SalvarParceiro(UsuarioDTO usuarioDTO);       
    
    }
}
