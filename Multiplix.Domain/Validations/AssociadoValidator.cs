using FluentValidation;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Validations
{
    public class AssociadoValidator : AbstractValidator<Associado>
    {
        private readonly IPatrocinadorRepository _patrocinadorRepository;
        private bool _isAtualizacao;

        public AssociadoValidator(IPatrocinadorRepository patrocinadorRepository, bool atualizando)
        {
            _patrocinadorRepository = patrocinadorRepository;
            _isAtualizacao = atualizando;

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

            RuleFor(associado => associado.PlanoAssinatura.Descricao)
            .NotEmpty()
            .WithMessage("O plano de assinatura é obrigatório");

            RuleFor(associado => associado.CPF)
             .NotEmpty()
             .WithMessage("O CPF é obrigatório.");
        }

      
    }
}
