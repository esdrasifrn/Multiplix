using FluentValidation;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Multiplix.Domain.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private bool _isAtualizacao;

        public UsuarioValidator(IUsuarioRepository usuarioRepository, bool atualizando)
        {
            _usuarioRepository = usuarioRepository;
            _isAtualizacao = atualizando;

            RuleFor(usuario => usuario.Nome)
               .NotEmpty()
               .WithMessage("O Nome é obrigatório.");

            RuleFor(usuario => usuario.Login)
              .NotEmpty()
              .WithMessage("O Login é obrigatório.")
              .Must((usuario, login) => LoginRotaJaExiste(usuario.Login))
              .When((usuario) => Atualizando(!_isAtualizacao))
              .WithMessage("O Login ja existe para outro associado.");

            RuleFor(usuario => usuario.Senha)
                .NotEmpty()
                .WithMessage("A Senha é obrigatória.");

            RuleFor(usuario => usuario.Email)
               .NotEmpty()
               .WithMessage("O email é obrigatório.");           
        }

        public bool LoginRotaJaExiste(string login)
        {
            return (!_usuarioRepository.LoginJaExiste(login));
        }
        public bool Atualizando(bool acao)
        {
            return acao;
        }
    }
}
