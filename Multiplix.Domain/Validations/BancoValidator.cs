using FluentValidation;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Validations
{
    public class BancoValidator : AbstractValidator<Banco>
    {
        public BancoValidator()
        {
            RuleFor(banco => banco.Nome)
               .NotEmpty()
               .WithMessage("O Nome é obrigatório.");

            RuleFor(banco => banco.Codigo)
               .NotEmpty()
               .WithMessage("O Código do banco é obrigatório.");
        }
    }
}
