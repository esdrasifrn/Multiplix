using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceUsuario : IServiceBase<Usuario>
    {
        Usuario Autenticar(string login, string password);
        ValidationResult Salvar(UsuarioDTO usuarioDTO);
        void RemoverUsuario(int usuarioId);
        ValidationResult SalvarPermissaoUsuario(UsuarioDTO usuarioDTO);
    }
}
