using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceCompra : IServiceBase<Compra>
    {
        ValidationResult Salvar(CompraDTO compraDTO);     
    }
}
