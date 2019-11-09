using FluentValidation;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Validations
{
    public class CompraValidator : AbstractValidator<Compra>
    {
        public  CompraValidator()
        {
            RuleFor(compra => compra.Valor)
               .NotEmpty()
               .WithMessage("O Valor é obrigatório.");

            //RuleFor(compra => compra.Parceiro)
            //    .NotEmpty()
            //    .WithMessage("O Parceiro é obrigatório.");

            RuleFor(compra => compra.Associado)
                .NotEmpty()
                .WithMessage("O Associado é obrigatório.");
        }
    }
}
