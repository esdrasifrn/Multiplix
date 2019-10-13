using FluentValidation;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Validations
{
    public class PlanoAssinaturaValidator : AbstractValidator<PlanoAssinatura>
    {
        public PlanoAssinaturaValidator()
        {
            RuleFor(planoAssinatura => planoAssinatura.Descricao)
               .NotEmpty()
               .WithMessage("A descrição do plano é obrigatória.");

            RuleFor(planoAssinatura => planoAssinatura.Valor)
               .NotEmpty()
               .WithMessage("O Valor do plano é obrigatório.");
        }
    }
}
