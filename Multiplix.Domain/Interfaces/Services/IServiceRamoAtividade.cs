using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceRamoAtividade : IServiceBase<RamoAtividade>
    {
        ValidationResult Salvar(RamoAtividadeDTO ramoAtividadeDTO);     
    }
}
