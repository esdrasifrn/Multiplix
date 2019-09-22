using FluentValidation;
using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario.Nome)
               .NotEmpty()
               .WithMessage("O Nome é obrigatório.");

            RuleFor(usuario => usuario.Login)
                .NotEmpty()
                .WithMessage("O Login é obrigatório.");

            RuleFor(usuario => usuario.Senha)
                .NotEmpty()
                .WithMessage("A Senha é obrigatória.");

            RuleFor(usuario => usuario.Email)
               .NotEmpty()
               .WithMessage("O email é obrigatório.");           
        }
    }
}
