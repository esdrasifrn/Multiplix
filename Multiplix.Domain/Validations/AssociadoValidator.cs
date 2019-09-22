using FluentValidation;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Validations
{
    public class AssociadoValidator : AbstractValidator<Associado>
    {
        public AssociadoValidator()
        {
            RuleFor(associado => associado.Usuario.Nome)
               .NotEmpty()
               .WithMessage("O Nome é obrigatório.");

            RuleFor(associado => associado.Usuario.Login)
                .NotEmpty()
                .WithMessage("O Login é obrigatório.");

            RuleFor(associado => associado.Usuario.Senha)
                .NotEmpty()
                .WithMessage("A Senha é obrigatória.");

            RuleFor(associado => associado.Usuario.Email)
               .NotEmpty()
               .WithMessage("O email é obrigatório.");

            RuleFor(associado => associado.Banco.Nome)
             .NotEmpty()
             .WithMessage("O banco é obrigatório.");
        }
    }
}
