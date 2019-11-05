using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using System.Collections.Generic;

namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceParceiro : IServiceBase<Parceiro>
    {
       
        ValidationResult SalvarParceiro(UsuarioDTO usuarioDTO);
        List<ListaProdutoParceiroDTO> ListaProdutosParceiroDTO(string pesquisa);
        List<ListaProdutoParceiroDTO> ListaProdutosParceiroDTO();

    }
}
    