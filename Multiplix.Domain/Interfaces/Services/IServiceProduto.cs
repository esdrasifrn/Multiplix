using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceProduto : IServiceBase<Produto>
    {
        ValidationResult Salvar(ProdutoDTO produtoDTO);
        ValidationResult SalvarProdutoParceiro(ProdutoParceiroDTO produtoParceiroDTO);
    }
}
