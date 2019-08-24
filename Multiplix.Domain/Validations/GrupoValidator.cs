using FluentValidation;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Validations
{
    public class GrupoValidator : AbstractValidator<Grupo>
    {
        public GrupoValidator()
        {
            RuleFor(grupo => grupo.Nome)
               .NotEmpty()
               .WithMessage("O Nome é obrigatório.");
        }
    }
}
