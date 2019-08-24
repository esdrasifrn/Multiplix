using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using System.Collections.Generic;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServicePermissao : IServiceBase<Permissao>
    {
        void SyncPermissao();
        List<PermissaoDTO> UsuarioObterTodasPermissoes(Usuario usuario);
    }
}