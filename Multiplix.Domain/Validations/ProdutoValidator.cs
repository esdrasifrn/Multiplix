using FluentValidation;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Validations
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(produto => produto.Descricao)
               .NotEmpty()
               .WithMessage("A descrição é obrigatória.");
        }
    }
}
