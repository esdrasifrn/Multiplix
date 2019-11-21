using FluentValidation;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Validations
{
    public class CidadeValidator : AbstractValidator<Cidade>
    {
        public CidadeValidator()
        {
            RuleFor(cidade => cidade.Descricao)
               .NotEmpty()
               .WithMessage("O nome da cidade é obrigatório.");
        }
    }
}
