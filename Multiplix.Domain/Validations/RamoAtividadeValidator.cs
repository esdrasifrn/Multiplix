using FluentValidation;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Validations
{
    public class RamoAtividadeValidator : AbstractValidator<RamoAtividade>
    {
        public RamoAtividadeValidator()
        {
            RuleFor(ramo => ramo.Nome)
               .NotEmpty()
               .WithMessage("O Nome é obrigatório.");
        }
    }
}
