using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceParceiro : IServiceBase<Parceiro>
    {
       
        ValidationResult SalvarParceiro(UsuarioDTO usuarioDTO);
        List<ListaProdutoParceiroDTO> ListaProdutosParceiroDTO(string pesquisa, int cidadeId);
        List<ListaProdutoParceiroDTO> ListaProdutosParceiroDTO(int cidadeId);

        List<ListaRepasseParceiroDTO> ListaRepasseParceiroDTO(DateTime dataInicio, DateTime dataFim);
        List<ListaRepasseParceiroDTO> ListaRepasseParceiroDTO(DateTime dataInicio, DateTime dataFim, int parceiroId);

    }
}
    