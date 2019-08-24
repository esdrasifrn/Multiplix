using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;

namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceGrupo : IServiceBase<Grupo>
    {
        ValidationResult Salvar(GrupoDTO grupoDTO);
        void RemoverGrupo(int grupoId);
        ValidationResult SalvarPermissaoGrupo(GrupoDTO grupoDTO);
    }
}
