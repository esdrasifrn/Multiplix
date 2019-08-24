using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServicePatrocinador : IServiceBase<Patrocinador>
    {
       
        ValidationResult SalvarPatrocinadorSemConvite(UsuarioDTO usuarioDTO);       
    
    }
}
