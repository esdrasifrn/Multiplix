using FluentValidation;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Validations
{
    public class EntradaValidator : AbstractValidator<Entrada>
    {
        public EntradaValidator()
        {
            RuleFor(entrada => entrada.Descricao)
               .NotEmpty()
               .WithMessage("O Descrição é obrigatória.");

            RuleFor(entrada => entrada.Valor)
               .NotEmpty()
               .WithMessage("O valor é obrigatório.");
        }
    }
}
